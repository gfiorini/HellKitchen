using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField]
    private Transform container;
    
    [SerializeField]
    private Transform template;

    private DeliveryManager deliveryManager;// = DeliveryManager.Instance;

    private void Awake() {
        template.gameObject.SetActive(false);
        
    }
    private void Start() {
        deliveryManager = DeliveryManager.Instance;
        deliveryManager.OnOrderCreated += OnOrderCreated;
        deliveryManager.OnOrderRemoved += OnOrderRemoved;
    }

    private void OnOrderCreated(object sender, EventArgs e) {
        UpdateVisual();
    }
    private void OnOrderRemoved(object sender, EventArgs e) {
        UpdateVisual();
    }    


    private void UpdateVisual() {
        foreach (Transform child in container){
            if (child != template){
                Destroy(child.gameObject);
            }
        }

        List<RecipeSO> waitingList = deliveryManager.GetWaitingList();
        foreach (RecipeSO recipeSo in waitingList){
            Transform recipe = Instantiate(template, container);
            recipe.gameObject.SetActive(true);
        }
    }
}
