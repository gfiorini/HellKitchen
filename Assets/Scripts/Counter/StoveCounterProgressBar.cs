using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveCounterProgressBar : MonoBehaviour
{
    [SerializeField]
    private GameObject hasProgressGameObject;
    
    private IHasProgress hasProgressObject;

    // Start is called before the first frame update
    [SerializeField]
    private Image progressBarImage;


    void Start() {
        hasProgressObject = hasProgressGameObject.GetComponent<IHasProgress>();
        hasProgressObject.OnProgressChange += StoveCounter_OnProgressChanged;
        progressBarImage.fillAmount = 0;
        Hide();
    }
    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangeArgs e) {
        progressBarImage.fillAmount = e.progressNormalized;
        if (progressBarImage.fillAmount == 0){
            Hide();
        } else {
            Show();
        }        
    }


    private void Hide() {
        gameObject.SetActive(false);
    }
    
    private void Show() {
        gameObject.SetActive(true);
    }    
}
