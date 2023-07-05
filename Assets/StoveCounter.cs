using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{

    [SerializeField]
    private KitchenObjectRecipeSO[] recipes;

    [SerializeField]
    private float cookTime = 5f;
    
    private float elapsedCookTime = 0f;

    private bool isCooking = false;
    private void Update() {
        if (isCooking){
            elapsedCookTime += Time.deltaTime;
            if (elapsedCookTime > cookTime){
                KitchenObjectRecipeSO cookedMeat = GetRecipe(GetKitchenObject().GetKitchenScriptableObject());
                GetKitchenObject().DestroySelf();
                AssignKitchenObject(cookedMeat.output, this);
                StopCooking();
            }
        }
        
        
    }

    public override void Interact(Player player) {
        if (GetKitchenObject() != null && player.GetKitchenObject() == null){
            GetKitchenObject().SetParent(player);
        } else if (GetKitchenObject() == null && player.GetKitchenObject() != null && ExistRecipe(player.GetKitchenObject())){
            player.GetKitchenObject().SetParent(this);
            StartCooking();
        }
         
    }
    private void StartCooking() {
        elapsedCookTime = 0;
        isCooking = true;
    }
    
    private void StopCooking() {
        elapsedCookTime = 0;
        isCooking = false;
    }    

    private KitchenObjectRecipeSO GetRecipe(KitchenObjectSO input) {
        foreach (var ko in recipes){
            if (ko.input.objectName == input.objectName){
                return ko;
            }
        }
        return null;
    }

    private bool ExistRecipe(KitchenObject currentKitchenObject) {
        return GetRecipe(currentKitchenObject.GetKitchenScriptableObject()) != null;
    }
}
