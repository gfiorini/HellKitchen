using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public override void Interact(Player player) {
        if (GetKitchenObject() == null){
            Transform t = Instantiate(GetKitchenObjectSO().prefab, GetKitchenObjectLocation());
            t.gameObject.GetComponent<KitchenObject>().SetParent(this);
        } else if (GetKitchenObject() != null && player.GetKitchenObject() == null){
            GetKitchenObject().SetParent(player);
        }
    }

}
