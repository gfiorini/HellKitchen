using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    
    private PlayerInputActions playerInputActions;

    public event EventHandler OnInteractHandler;
    public event EventHandler OnAlternateInteractHandler;
    public event EventHandler OnPauseHandler;
    
    public enum Binding
    {
        MOVE_UP,
        MOVE_DOWN,
        MOVE_LEFT,
        MOVE_RIGHT,
        INTERACT,
        ALT_INTERACT,
        PAUSE,
        INTERACT_PS,
        ALT_INTERACT_PS,
        PAUSE_PS        
    }
    
    private void Awake() {
        Instance = this;
        playerInputActions = new PlayerInputActions();

        if (PlayerPrefs.HasKey(Preferences.KEY_MAPPING.ToString())){
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(Preferences.KEY_MAPPING.ToString()));    
        }
            
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interaction.performed += InteractionOnperformed;
        playerInputActions.Player.AlternateInteraction.performed += AlternateInteractionPerformed;
        playerInputActions.Player.Pause.performed += OnPausePerformed;
    }    
    
    public String GetBindingText(Binding binding) {
        switch (binding){
            case Binding.MOVE_UP:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.MOVE_DOWN:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.MOVE_LEFT:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.MOVE_RIGHT:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.INTERACT:
                return playerInputActions.Player.Interaction.bindings[0].ToDisplayString();
            case Binding.ALT_INTERACT:
                return playerInputActions.Player.AlternateInteraction.bindings[0].ToDisplayString();
            case Binding.PAUSE:
                String s = playerInputActions.Player.Pause.bindings[0].ToDisplayString();
                if (s.Length > 3){
                    s = s.Substring(0, 3);
                }
                return s;
            case Binding.INTERACT_PS:
                return playerInputActions.Player.Interaction.bindings[1].ToDisplayString();
            case Binding.ALT_INTERACT_PS:
                return playerInputActions.Player.AlternateInteraction.bindings[1].ToDisplayString();
            case Binding.PAUSE_PS:
                return playerInputActions.Player.Pause.bindings[1].ToDisplayString();
       
        }

        throw new Exception("Undefined Binding");
    }

    public void Rebind(Binding binding, Action onRebound) {

        InputAction inputAction;
        int index;

        switch (binding){
            case Binding.MOVE_UP:
                inputAction = playerInputActions.Player.Move;
                index = 1;
                Rebind(inputAction, index, onRebound);
                break;
            case Binding.MOVE_DOWN:
                inputAction = playerInputActions.Player.Move;
                index = 2;
                Rebind(inputAction, index, onRebound);
                break;
            case Binding.MOVE_LEFT:
                inputAction = playerInputActions.Player.Move;
                index = 3;
                Rebind(inputAction, index, onRebound);
                break;
            case Binding.MOVE_RIGHT:
                inputAction = playerInputActions.Player.Move;
                index = 4;
                Rebind(inputAction, index, onRebound);
                break;   
            case Binding.INTERACT:
                inputAction = playerInputActions.Player.Interaction;
                index = 0;
                Rebind(inputAction, index, onRebound);
                break;                
            case Binding.ALT_INTERACT:
                inputAction = playerInputActions.Player.AlternateInteraction;
                index = 0;
                Rebind(inputAction, index, onRebound);
                break; 
            case Binding.PAUSE:
                inputAction = playerInputActions.Player.Pause;
                index = 0;
                Rebind(inputAction, index, onRebound);
                break; 
            case Binding.INTERACT_PS:
                inputAction = playerInputActions.Player.Interaction;
                index = 1;
                Rebind(inputAction, index, onRebound);
                break;                
            case Binding.ALT_INTERACT_PS:
                inputAction = playerInputActions.Player.AlternateInteraction;
                index = 1;
                Rebind(inputAction, index, onRebound);
                break; 
            case Binding.PAUSE_PS:
                inputAction = playerInputActions.Player.Pause;
                index = 1;
                Rebind(inputAction, index, onRebound);
                break;             
        }
    }
    private void Rebind(InputAction inputAction, int rebindIndex, Action onRebound) { 
        
        playerInputActions.Player.Disable();
        inputAction.PerformInteractiveRebinding(rebindIndex).
                    OnComplete((callback) => {
                        callback.Dispose();
                        playerInputActions.Player.Enable();
                        string json = playerInputActions.SaveBindingOverridesAsJson();
                        PlayerPrefs.SetString(Preferences.KEY_MAPPING.ToString(), json);
                        PlayerPrefs.Save();
                        onRebound();
                    }).
                    Start(); 
    }

    private void OnPausePerformed(InputAction.CallbackContext obj) {
        OnPauseHandler?.Invoke(obj, EventArgs.Empty);
    }
    private void AlternateInteractionPerformed(InputAction.CallbackContext obj) {
        OnAlternateInteractHandler?.Invoke(obj, EventArgs.Empty);
    }

    private void InteractionOnperformed(InputAction.CallbackContext obj) {
        OnInteractHandler?.Invoke(obj, EventArgs.Empty);
    }

    public Vector2 GetNormalizedInput() {
        Vector2 control = playerInputActions.Player.Move.ReadValue<Vector2>();
        return control;
    }

}
