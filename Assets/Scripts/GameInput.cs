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
