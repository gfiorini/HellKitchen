using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{

    [SerializeField]
    private KitchenObjectRecipeSO[] recipes;
    
    public override void Interact(Player player) {
        if (GetKitchenObject() != null && player.GetKitchenObject() == null){
            GetKitchenObject().SetParent(player);
        } else if (GetKitchenObject() == null && player.GetKitchenObject() != null && ExistRecipe(player.GetKitchenObject())){
            player.GetKitchenObject().SetParent(this);
        }
         
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
