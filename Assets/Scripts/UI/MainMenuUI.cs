using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private Button playButton;
    
    [SerializeField]
    private Button quitButton;

    private void Awake() {
        
        //reset pause
        Time.timeScale = 1f;
        
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });

        playButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.GameScene);
        });

    }
    
    
}
