using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField]
    private Counter counter;

    [SerializeField]
    private GameObject visual;

    private void Start() {

         Player.Instance.OnSelectedCounterChanged += PlayerOnOnSelectedCounterChanged;
    }
    private void PlayerOnOnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedArgs e) {
        //Debug.Log("PlayerOnOnSelectedCounterChanged");
        if (counter == e.selectedCounter){
            visual.SetActive(true);
        } else{
            visual.SetActive(false);
        }

    }
    
}
