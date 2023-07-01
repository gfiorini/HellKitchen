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
    private Image progressBarImage;


    void Start() {
        cutterCounter.OnProgressChange += RefreshProgressBar;
        progressBarImage = GetComponent<Image>();
    }
    private void RefreshProgressBar(object sender, CutterCounter.OnProgressChangeArgs e) {
        progressBarImage.fillAmount = 1 - e.progressNormalized;
    }



}
