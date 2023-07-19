using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{

    [SerializeField]
    private StoveCounter stoveCounter;

    private AudioSource buzz;

    private void Awake() {
        buzz = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnStateChanged += StoveCounterOnOnStateChanged;
    }
    private void StoveCounterOnOnStateChanged(object sender, StoveCounter.OnStateChangedArgs e) {
        if (e.state == StoveCounter.StoveState.COOKING || e.state == StoveCounter.StoveState.OVERCOOKING){
            buzz.Play();
        } else{
            buzz.Pause();
        }
    }


}
