using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{

    private IHasProgress hasProgress;
    
    [SerializeField]
    private GameObject hasProgressGameObject;

    [SerializeField]
    private GameObject[] visuals;

   
    private void Start() {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        hasProgress.OnProgressChange += StoveCounter_OnProgressChanged;
    }
    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangeArgs e) {
        bool flgActive = e.progressNormalized > 0;
        foreach (var visual in visuals){
            visual.SetActive(flgActive);    
        } 
    }

}
