using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour,  IParentable
{
    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;

    [SerializeField]
    private Transform spawnLocation;

    private KitchenObject kitchenObject;

    public static event EventHandler OnDroppedObject;

    public virtual void Interact(Player player) {

    }

    public static void ResetEvents() {
        OnDroppedObject = null;
    }
    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }
    
    public void  SetKitchenObject(KitchenObject ko) {
        if (ko != null){
            OnDroppedObject?.Invoke(this.gameObject, EventArgs.Empty);
        }
        this.kitchenObject = ko;
    }

    public Transform GetKitchenObjectLocation() {
        return spawnLocation;
    }

    protected KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    protected void AssignKitchenObject(KitchenObjectSO kitchenObjectSO, IParentable parent) {
        Transform t = Instantiate(kitchenObjectSO.prefab);
        t.gameObject.GetComponent<KitchenObject>().SetParent(parent);
    }
    

    public virtual void AlternateInteract(Player player) {
        //Debug.Log("AlternateInteract");
    }
    
    
}
