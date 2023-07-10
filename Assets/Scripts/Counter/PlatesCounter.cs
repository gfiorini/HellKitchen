using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{

    public event EventHandler OnPlateSpawn;

    public event EventHandler OnPlatePickup;

    private const int MAX_NUM_PLATES = 4;

    private const float SPAWN_TIMER = 4f;

    private int numPlates;

    private float timer;
    private void Update(){
        if (numPlates < MAX_NUM_PLATES){
            timer += Time.deltaTime;
            if (timer > SPAWN_TIMER){
                numPlates++;
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
                timer = 0;
            }
        }
    }

    public override void Interact(Player player){
        if (numPlates > 0 && player.GetKitchenObject() == null){
            AssignKitchenObject(GetKitchenObjectSO(), player);
            numPlates--;
            OnPlatePickup?.Invoke(this, EventArgs.Empty);
        }
    }


}
