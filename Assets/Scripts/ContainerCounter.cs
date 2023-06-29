using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{

    public event EventHandler OnOpenContainerCounter; 

    public override void Interact(Player player) {
        if (GetKitchenObject() == null && player.GetKitchenObject() == null){
            Transform t = Instantiate(GetKitchenObjectSO().prefab, GetKitchenObjectLocation());
            t.gameObject.GetComponent<KitchenObject>().SetParent(player);
            OnOpenContainerCounter?.Invoke(this, EventArgs.Empty);
        } 
    }

}
