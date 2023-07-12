using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    public override void Interact(Player player) {
        if (GetKitchenObject() != null && player.GetKitchenObject() == null){
            GetKitchenObject().SetParent(player);
        } else if (GetKitchenObject() == null && player.GetKitchenObject() != null){
            player.GetKitchenObject().SetParent(this);
        } else if (GetKitchenObject() != null && player.GetKitchenObject() != null){
            if (player.GetKitchenObject().TryGetPlate(out Plate plate)){
                if (plate.TryAddIngredient(GetKitchenObject().GetKitchenScriptableObject())){
                    GetKitchenObject().DestroySelf();
                }
            }
        }
    }

    
}




