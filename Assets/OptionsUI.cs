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
    private Button interactButtonPS;

    [SerializeField]
    private Button alternateInteractButtonPS;

    [SerializeField]
    private Button pauseButtonPS;
    
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
    private TextMeshProUGUI interactLabelPS;

    [SerializeField]
    private TextMeshProUGUI alternateInteractLabelPS;

    [SerializeField]
    private TextMeshProUGUI pauseLabelPS;    

    [SerializeField]
    private Transform keyPopup;

    [SerializeField]
    private Button musicVolumeButton;

    [SerializeField]
    private TextMeshProUGUI musicLabel;

    private Action onClose;
    
    private void Awake() {
        Instance = this;
        HidePopup();
        soundEffectsVolumeButton.onClick.AddListener(() => { ChangeSFXVolume(); });
        musicVolumeButton.onClick.AddListener(() => { ChangeMusicVolume(); });
        moveUpButton.onClick.AddListener(() => { RemapKey(GameInput.Binding.MOVE_UP); });
        moveDownButton.onClick.AddListener(() => { RemapKey(GameInput.Binding.MOVE_DOWN); });
        moveLeftButton.onClick.AddListener(() => { RemapKey(GameInput.Binding.MOVE_LEFT); });
        moveRightButton.onClick.AddListener(() => { RemapKey(GameInput.Binding.MOVE_RIGHT); });
        interactButton.onClick.AddListener(() => { RemapKey(GameInput.Binding.INTERACT); });
        alternateInteractButton.onClick.AddListener(() => { RemapKey(GameInput.Binding.ALT_INTERACT); });
        pauseButton.onClick.AddListener(() => { RemapKey(GameInput.Binding.PAUSE); });        
        interactButtonPS.onClick.AddListener(() => { RemapKey(GameInput.Binding.INTERACT_PS); });
        alternateInteractButtonPS.onClick.AddListener(() => { RemapKey(GameInput.Binding.ALT_INTERACT_PS); });
        pauseButtonPS.onClick.AddListener(() => { RemapKey(GameInput.Binding.PAUSE_PS); });
        exitButton.onClick.AddListener(() => { Hide(); });
    }
    private void HidePopup() {
        keyPopup.gameObject.SetActive(false);
    }
    
    private void ShowPopup() {
        keyPopup.gameObject.SetActive(true);
        soundEffectsVolumeButton.Select();
    }    
    private void RemapKey(GameInput.Binding binding) {
        ShowPopup();
        GameInput.Instance.Rebind(binding, onRebound);
    }
    private void onRebound() {
        HidePopup();
        UpdateVisual();
    }
    private void ChangeMusicVolume() {
        MusicManager.Instance.ChangeVolume();
        UpdateVisual();
    }
    private void ChangeSFXVolume() {
        SFXManager.Instance.ChangeVolume();
        UpdateVisual();
    }
    private void UpdateVisual() {
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
        interactLabelPS.text = GameInput.Instance.GetBindingText(GameInput.Binding.INTERACT_PS);
        alternateInteractLabelPS.text = GameInput.Instance.GetBindingText(GameInput.Binding.ALT_INTERACT_PS);
        pauseLabelPS.text = GameInput.Instance.GetBindingText(GameInput.Binding.PAUSE_PS);
    }
    void Start() {
        GameManager.Instance.OnResume += OnResume;
        Hide();
        UpdateVisual();
    }
    private void OnDestroy() {
        GameManager.Instance.OnResume -= OnResume;
    }    
    private void OnResume(object sender, EventArgs e) {
        Hide();
    }
    private void Hide() {
        gameObject.SetActive(false);
        if (onClose != null) onClose();
    }
    public void Show(Action onClose) {
        this.onClose = onClose;
        gameObject.SetActive(true);
        soundEffectsVolumeButton.Select();
    }
    
}
