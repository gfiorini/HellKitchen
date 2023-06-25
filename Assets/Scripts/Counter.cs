using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour, IParentable
{
    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;


    [SerializeField]
    private Counter secondCounter;
    
    [SerializeField]
    private Transform spawnLocation;

    private KitchenObject kitchenObject;

    private bool test = true;
    private void Update() {
        if (test && Input.GetKeyDown(KeyCode.T) && this.name == "Right"){
            //Debug.Log(("T pressed!"));
            if (kitchenObject != null){
                kitchenObject.SetParent(secondCounter); 
                //Debug.Log(kitchenObject.GetCounter());
            } else{
                Debug.Log("kitchenObject is null!");
            }
        }
    }

    public void Interact(Player player) {
        if (kitchenObject == null){
            if (player.GetKitchenObject() == null){
                Transform t = Instantiate(kitchenObjectSO.prefab, spawnLocation);
                t.gameObject.GetComponent<KitchenObject>().SetParent(this);
            } else{
                player.GetKitchenObject().SetParent(this);
            }
        } else{
            kitchenObject.SetParent(player);
        }
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }
    
    public void  SetKitchenObject(KitchenObject ko) {
        this.kitchenObject = ko;
    }

    public Transform GetKitchenObjectLocation() {
        return spawnLocation;
    }
    
}


