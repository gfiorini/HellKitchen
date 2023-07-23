using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    
    public static OptionsUI Instance { get; private set; }
    
    [SerializeField]
    private Button exitButton;

    private void Awake() {
        Instance = this;
        exitButton.onClick.AddListener(() => {
            Hide();
        });
    }
    void Start() {
        GameManager.Instance.OnResume += OnResume;
        Hide();
    }
    private void OnResume(object sender, EventArgs e) {
        Hide();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
    public void Show() {
        gameObject.SetActive(true);
    }
    
}
