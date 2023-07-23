using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameUI : MonoBehaviour
{
    [SerializeField]
    private Button resumeButton;
    
    [SerializeField]
    private Button mainMenuButton;
    void Start() {
        Hide();
        GameManager.Instance.OnPause += OnPause;
        GameManager.Instance.OnResume += OnResume;
        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.TogglePause();
        });
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });        
    }
    private void OnResume(object sender, EventArgs e) {
        Hide();
    }
    private void OnPause(object sender, EventArgs e) {
        Show();
    }
    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
    
}
