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
        PAUSE
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
        }

        throw new Exception("Undefined Binding");
    }
    
    private void Awake() {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interaction.performed += InteractionOnperformed;
        playerInputActions.Player.AlternateInteraction.performed += AlternateInteractionPerformed;
        playerInputActions.Player.Pause.performed += OnPausePerformed;
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
