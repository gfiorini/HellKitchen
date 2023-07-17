using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{
    [SerializeField]
    private Transform imageContainer;

    [SerializeField]
    private Transform imageTemplate; 
    
    [SerializeField]
    private TextMeshProUGUI recipeName;

    private void Awake() {
        imageTemplate.gameObject.SetActive(false);
    }

    public void SetRecipe(RecipeSO recipe) {
        recipeName.SetText(recipe.name);

        foreach (KitchenObjectSO recipeIngredient in recipe.ingredients){
            Transform iconImage = Instantiate(imageTemplate, imageContainer);
            iconImage.GetComponent<Image>().sprite = recipeIngredient.icon;
            iconImage.gameObject.SetActive(true);
        }
        
    }
}
