using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    public event EventHandler OnInteractHandler;
    public event EventHandler OnAlternateInteractHandler;
    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interaction.performed += InteractionOnperformed;
        playerInputActions.Player.AlternateInteraction.performed += AlternateInteractionPerformed;
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
