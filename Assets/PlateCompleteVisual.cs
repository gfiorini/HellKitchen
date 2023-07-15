using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [SerializeField]
    private Plate plate;
    // Start is called before the first frame update
    [SerializeField]
    private List<KitchenObjectSO_GameObject> visuals;

    [Serializable]
    public struct KitchenObjectSO_GameObject  
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    
    void Start()
    {
        foreach (var s in visuals){
                s.gameObject.SetActive(false);
        }
        plate.OnAddIngredient += PlateOnOnAddIngredient;
    }
    private void PlateOnOnAddIngredient(object sender, Plate.IngredientEventArgs e) {
        foreach (var s in visuals){
            if (s.kitchenObjectSO == e.ingredient){
                s.gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
