using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private CutterCounter cutterCounter;

    // Start is called before the first frame update
    [SerializeField]
    private Image progressBarImage;


    void Start() {
        cutterCounter.OnProgressChange += CuttingCounter_OnProgressChanged;
        progressBarImage.fillAmount = 0;
        Hide();
    }
    private void CuttingCounter_OnProgressChanged(object sender, CutterCounter.OnProgressChangeArgs e) {
        progressBarImage.fillAmount = 1 - e.progressNormalized;
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