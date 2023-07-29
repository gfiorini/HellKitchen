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
    private Button moveUpButton;
    
    [SerializeField]
    private Button moveDownButton;

    [SerializeField]
    private Button moveLeftButton;

    [SerializeField]
    private Button moveRightButton;

    [SerializeField]
    private Button interactButton;

    [SerializeField]
    private Button alternateInteractButton;

    [SerializeField]
    private Button pauseButton;
    
    [SerializeField]
    private TextMeshProUGUI moveUpLabel;
    
    [SerializeField]
    private TextMeshProUGUI moveDownLabel;

    [SerializeField]
    private TextMeshProUGUI moveLeftLabel;

    [SerializeField]
    private TextMeshProUGUI moveRightLabel;

    [SerializeField]
    private TextMeshProUGUI interactLabel;

    [SerializeField]
    private TextMeshProUGUI alternateInteractLabel;

    [SerializeField]
    private TextMeshProUGUI pauseLabel;

    
    
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

        moveUpLabel.text = GameInput.Instance.GetBindingText(GameInput.Binding.MOVE_UP);
        moveDownLabel.text = GameInput.Instance.GetBindingText(GameInput.Binding.MOVE_DOWN);
        moveLeftLabel.text = GameInput.Instance.GetBindingText(GameInput.Binding.MOVE_LEFT);
        moveRightLabel.text = GameInput.Instance.GetBindingText(GameInput.Binding.MOVE_RIGHT);
        interactLabel.text = GameInput.Instance.GetBindingText(GameInput.Binding.INTERACT);
        alternateInteractLabel.text = GameInput.Instance.GetBindingText(GameInput.Binding.ALT_INTERACT);
        pauseLabel.text = GameInput.Instance.GetBindingText(GameInput.Binding.PAUSE);
        

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
