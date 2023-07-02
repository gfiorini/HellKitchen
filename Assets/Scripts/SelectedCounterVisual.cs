using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField]
    private BaseCounter selectedCounter;

    [SerializeField]
    private GameObject[] visuals;

    private void Start() {
         Player.Instance.OnSelectedCounterChanged += PlayerOnOnSelectedCounterChanged;
    }
    private void PlayerOnOnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedArgs e) {
        bool flgActive = (selectedCounter == e.SelectedCounter);
        foreach (var visual in visuals){
            visual.SetActive(flgActive);    
        } 
    }
    
}
