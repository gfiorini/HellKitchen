using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipeUI : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI recipeName;


    public void SetText(RecipeSO recipe) {
        recipeName.SetText(recipe.name);
    }
}
