using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{

    [SerializeField]
    private StoveCounter stoveCounter;

    private AudioSource buzz;

    private const float FREQ = 0.2f;

    private float timer = 0;

    private void Awake() {
        buzz = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnStateChanged += StoveCounterOnOnStateChanged;
        stoveCounter.OnProgressChange += OnProgressChanged;
    }
    private void OnProgressChanged(object sender, IHasProgress.OnProgressChangeArgs e) {
        if (stoveCounter.GetState() == StoveCounter.StoveState.OVERCOOKING && e.progressNormalized > 0.5f){
            PlayWarningSound();
        } 
    }
    private void PlayWarningSound() {
        timer += Time.deltaTime;
        if (timer > FREQ){
            timer = 0;
            SFXManager.Instance.PlayWarning(transform.position);    
        }
    }

    private void StoveCounterOnOnStateChanged(object sender, StoveCounter.OnStateChangedArgs e) {
        if (e.state == StoveCounter.StoveState.COOKING || e.state == StoveCounter.StoveState.OVERCOOKING){
            buzz.Play();
        } else{
            buzz.Pause();
        }
    }


}
