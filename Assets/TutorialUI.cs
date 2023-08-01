using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI keyLeft;
    
    [SerializeField]
    private TextMeshProUGUI keyRight;
    
    [SerializeField]
    private TextMeshProUGUI keyUp;
    
    [SerializeField]
    private TextMeshProUGUI keyDown;
    
    [SerializeField]
    private TextMeshProUGUI keyInteract;
    
    [SerializeField]
    private TextMeshProUGUI keyAltInteract;
    
    [SerializeField]
    private TextMeshProUGUI keyPause;    
    
    //Gamepad
    [SerializeField]
    private TextMeshProUGUI keyGamePadInteract;
    
    [SerializeField]
    private TextMeshProUGUI keyGamePadAltInteract;
    
    [SerializeField]
    private TextMeshProUGUI keyGamePadPause;      
    
    void Start() {
        GameManager.Instance.OnGameStateChange += OnGameStateChange;
        Show();
    }
    private void OnGameStateChange(object sender, GameManager.EventGameState e) {
        if (e.state == GameManager.GameState.COUNTDOWN){
            Hide();
        }
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
    
    private void Show() {
        gameObject.SetActive(true);
        UpdateVisuals();
    }    

    private void UpdateVisuals() {
        keyUp.text = GameInput.Instance.GetBindingText(GameInput.Binding.MOVE_UP);
        keyDown.text = GameInput.Instance.GetBindingText(GameInput.Binding.MOVE_DOWN);
        keyLeft.text = GameInput.Instance.GetBindingText(GameInput.Binding.MOVE_LEFT);
        keyRight.text = GameInput.Instance.GetBindingText(GameInput.Binding.MOVE_RIGHT);
        keyInteract.text = GameInput.Instance.GetBindingText(GameInput.Binding.INTERACT);
        keyAltInteract.text = GameInput.Instance.GetBindingText(GameInput.Binding.ALT_INTERACT);
        keyPause.text = GameInput.Instance.GetBindingText(GameInput.Binding.PAUSE);
        keyGamePadInteract.text = GameInput.Instance.GetBindingText(GameInput.Binding.INTERACT_PS);
        keyGamePadAltInteract.text = GameInput.Instance.GetBindingText(GameInput.Binding.ALT_INTERACT_PS);
        keyGamePadPause.text = GameInput.Instance.GetBindingText(GameInput.Binding.PAUSE_PS);        
    }


}
