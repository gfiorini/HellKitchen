using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    private const string POPUP = "Popup";

    private Animator _animator;
    
    [SerializeField]
    private Image background;

    [SerializeField]
    private TextMeshProUGUI label;
    
    [SerializeField]
    private Image icon;

    [SerializeField]
    private Sprite successIcon;
    
    [SerializeField]
    private Sprite failIcon;    
    
    [SerializeField]
    private Color failBackgroundColor;

    [SerializeField]
    private Color successBackgroundColor;
    
    void Start() {
        DeliveryManager.Instance.OnOrderSuccess += OnSuccess;
        DeliveryManager.Instance.OnOrderFailed += OnFail;
        _animator = GetComponent<Animator>();
        Hide();
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
    
    private void Show() {
        gameObject.SetActive(true);
    }
    
    private void OnFail(object sender, EventArgs e) {
        background.color = failBackgroundColor;
        label.text = "Delivery\n Failed!";
        icon.sprite = failIcon;
        _animator.SetTrigger(POPUP);
        Show();
    }
    private void OnSuccess(object sender, EventArgs e) {
        background.color = successBackgroundColor;
        label.text = "Delivery\n Success!";
        icon.sprite = successIcon;
        _animator.SetTrigger(POPUP);
        Show();
    }

}
