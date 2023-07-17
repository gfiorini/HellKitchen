using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObjectWithProgress;
    
    private IHasProgress hasProgress;

    // Start is called before the first frame update
    [SerializeField]
    private Image progressBarImage;


    void Start() {
        hasProgress = gameObjectWithProgress.GetComponent<IHasProgress>();
        hasProgress.OnProgressChange += OnProgressChanged;
        progressBarImage.fillAmount = 0;
        Hide();
    }
    private void OnProgressChanged(object sender, IHasProgress.OnProgressChangeArgs e) {
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
