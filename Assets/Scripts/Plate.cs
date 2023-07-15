using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : KitchenObject
{

    public event EventHandler<IngredientEventArgs> OnAddIngredient;
    public class IngredientEventArgs : EventArgs {
        public KitchenObjectSO ingredient;
    }
        
    [SerializeField]
    private List<KitchenObjectSO> admittedKitchenObjects;

    private List<KitchenObjectSO> list;

    private void Awake(){
        list = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO koSO){
        if (admittedKitchenObjects.Contains(koSO) && !list.Contains(koSO)){
            list.Add(koSO);
            OnAddIngredient?.Invoke(this, new IngredientEventArgs {
                ingredient = koSO
            });
            return true;
        } else {
            return false;
        }
        
    }
}
