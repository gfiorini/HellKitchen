using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    
    public static OptionsUI Instance { get; private set; }
    
    [SerializeField]
    private Button exitButton;

    [SerializeField]
    private Button soundEffectsVolumeButton;

    [SerializeField]
    private TextMeshProUGUI soundEffectsLabel;
    
    [SerializeField]
    private Button musicVolumeButton;

    [SerializeField]
    private TextMeshProUGUI musicLabel;
    
    private void Awake() {
        Instance = this;
        exitButton.onClick.AddListener(() => {
            Hide();
        });
        musicVolumeButton.onClick.AddListener(() => {
            ChangeMusicVolume();
        });
        soundEffectsVolumeButton.onClick.AddListener(() => {
            ChangeSFXVolume();
        });              
    }
    
    
    private void ChangeMusicVolume() {
        MusicManager.Instance.ChangeVolume();
        UpdateLabel();
    }
    private void ChangeSFXVolume() {
        SFXManager.Instance.ChangeVolume();
        UpdateLabel();
    }
    private void UpdateLabel() {
        float sfxVolume = SFXManager.Instance.GetVolume();
        float musicVolume = MusicManager.Instance.GetVolume();
        soundEffectsLabel.text = "Sound Effects Volume: " + Math.Round(sfxVolume * 10).ToString();
        musicLabel.text = "Music Volume: " + Math.Round(musicVolume * 10).ToString();
    }
    void Start() {
        GameManager.Instance.OnResume += OnResume;
        Hide();
        UpdateLabel();
    }
    
    private void OnDestroy() {
        GameManager.Instance.OnResume -= OnResume;
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
