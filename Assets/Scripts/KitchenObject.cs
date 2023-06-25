using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{

    [SerializeField]
    private KitchenObjectSO kitchenScriptableObject;

    private IParentable parent;
    
    public KitchenObjectSO GetKitchenScriptableObject() {
        return kitchenScriptableObject;
    }

    public IParentable GetParent() {
        return parent;
    }
    public void SetParent(IParentable p) {
        
//        Debug.Log("SetCounter", c);
        //rimuovo il riferimento del vecchio oggetto dal counter
        if (this.parent != null){
            this.parent.SetKitchenObject(null);
        }

        //imposto il counter corrente
        this.parent = p;
        
        if (p.GetKitchenObject() != null){
            Debug.LogError("Counter has already a kitchen object");
        }
        
        this.parent.SetKitchenObject(this);
        
        //aggiorno la posizione visuale dell'oggetto
        transform.parent = p.GetKitchenObjectLocation();
        transform.localPosition = Vector3.zero;
    }
    
    
}
