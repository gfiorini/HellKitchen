using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{

    public static event EventHandler OnTrash;
    
    public override void Interact(Player player) {
        if (player.GetKitchenObject() != null){
            OnTrash?.Invoke(gameObject, EventArgs.Empty);
            player.GetKitchenObject().DestroySelf();
        }
    }
    
    public new static void ResetEvents() {
        OnTrash = null;
    }

    
}




