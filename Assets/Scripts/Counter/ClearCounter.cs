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
            //ho un piatto in mano e cerco di raccogliere un oggetto
            if (player.GetKitchenObject().TryGetPlate(out Plate plate))            {
                if (plate.TryAddIngredient(GetKitchenObject().GetKitchenScriptableObject()))                {
                    GetKitchenObject().DestroySelf();
                }
            } else {
                // ho un oggetto in mano che non è un piatto,
                // vedo se sul container c'e' un piatto e cerco di aggiungere l'ingrediente
                if (GetKitchenObject().TryGetPlate(out Plate p)){
                    if (p.TryAddIngredient(player.GetKitchenObject().GetKitchenScriptableObject())){
                        player.GetKitchenObject().DestroySelf();
                    }
                }
            }
        }
    }

    
}




