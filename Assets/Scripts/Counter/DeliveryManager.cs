using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField]
    private RecipeCollectionSO recipeCollection;
    public static DeliveryManager Instance {get; private set; }

    public event EventHandler OnOrderCreated;
    public event EventHandler OnOrderRemoved;
    
    private float SPAWN_TIMER = 4f;
    private int MAX_RECIPES = 3;
    private float currentSpawnTimer = 4f;
    private int currentNumRecipes;
    
    private List<RecipeSO> waitingList;

    private void Awake() {
        waitingList = new List<RecipeSO>();
        if (Instance != null){
            throw new Exception("DeliveryManager not null!");
        } else{
            Instance = this;
        }
    }

    private void Update() {
        if (waitingList.Count <= MAX_RECIPES){
            currentSpawnTimer += Time.deltaTime;
            if (currentSpawnTimer >= SPAWN_TIMER){
                RecipeSO recipe = recipeCollection.recipes[Random.Range(0, recipeCollection.recipes.Count)];
                waitingList.Add(recipe);
                OnOrderCreated?.Invoke(this, EventArgs.Empty);
                currentSpawnTimer = 0;
            }
        }
    }


    public void TryDeliver(Plate plate) {
        RecipeSO foundRecipe = null;
        List<KitchenObjectSO> deliveryIngredients = plate.GetIngredients();
        foreach (RecipeSO recipe in waitingList){
            if (IsValidRecipe(deliveryIngredients, recipe)){
                foundRecipe = recipe;
                break;
            }
        }
        if (foundRecipe != null){
            waitingList.Remove(foundRecipe);
            OnOrderRemoved?.Invoke(this, EventArgs.Empty);
            plate.DestroySelf();
        }
    }
    private bool IsValidRecipe(List<KitchenObjectSO> deliveryIngredients, RecipeSO recipe) {
        List<KitchenObjectSO> recipeIngredients = recipe.ingredients;
        if (recipeIngredients.Count != deliveryIngredients.Count){
            return false;
        } else {
            foreach (KitchenObjectSO deliveryIngredient in deliveryIngredients){
                bool found = false;
                foreach (KitchenObjectSO recipeIngredient in recipeIngredients){
                    if (recipeIngredient == deliveryIngredient){
                        found = true;
                        break;
                    }
                }
                if (!found){
                    return false;
                }
            }
            return true;
        }
    }

    public List<RecipeSO> GetWaitingList() {
        return waitingList;
    }

}
