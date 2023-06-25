using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IParentable 
{

    void SetKitchenObject(KitchenObject kitchenObject);
    KitchenObject GetKitchenObject();
    Transform GetKitchenObjectLocation();
}
