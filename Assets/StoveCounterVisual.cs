using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField]
    private StoveCounter counter;

    [SerializeField]
    private GameObject[] visuals;

   
    private void Start() {
        counter.OnProgressChange += StoveCounter_OnProgressChanged;
    }
    private void StoveCounter_OnProgressChanged(object sender, StoveCounter.OnProgressChangeArgs e) {
        bool flgActive = e.normalizedTime > 0;
        foreach (var visual in visuals){
            visual.SetActive(flgActive);    
        } 
    }

}
