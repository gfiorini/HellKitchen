using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterWarningUI : MonoBehaviour
{

    [SerializeField]
    private StoveCounter _stoveCounter;
    void Start() {
        _stoveCounter.OnProgressChange += StoveCounterOnOnProgressChange;
        Hide();
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
    
    private void Show() {
        gameObject.SetActive(true);
    }
    
    private void StoveCounterOnOnProgressChange(object sender, IHasProgress.OnProgressChangeArgs e) {
        if (_stoveCounter.GetState() == StoveCounter.StoveState.OVERCOOKING && e.progressNormalized > 0.5f){
            Show(); 
        } else{
            Hide();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
