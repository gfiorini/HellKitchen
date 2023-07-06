using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class StoveCounter : BaseCounter
{

    public enum StoveState {
        IDLE,
        COOKING,
        OVERCOOKING
    }
    
    [SerializeField]
    private StoveKitchenObjectRecipeSO[] recipes;

    private float elapsedCookTime = 0f;

    private StoveKitchenObjectRecipeSO currentRecipe;

    private StoveState state = StoveState.IDLE;
    
    private void Update() {
        switch (state){
            case StoveState.COOKING:
                Cooking();
                break;
            case StoveState.OVERCOOKING:
                OverCooking();
                break;
        }
    }
    
    private void Cooking() {
        elapsedCookTime += Time.deltaTime;
        if (elapsedCookTime > currentRecipe.timer){
            GetKitchenObject().DestroySelf();
            KitchenObjectSO ko = currentRecipe.output;
            AssignKitchenObject(ko, this);
            currentRecipe = GetRecipe(ko);
            state = StoveState.OVERCOOKING;
            elapsedCookTime = 0;
        }
    }

    private void OverCooking() {
        elapsedCookTime += Time.deltaTime;
        if (elapsedCookTime > currentRecipe.timer){
            GetKitchenObject().DestroySelf();
            AssignKitchenObject(currentRecipe.output, this);
            currentRecipe = null;
            state = StoveState.IDLE;
        }        
    }

    public override void Interact(Player player) {
        if (GetKitchenObject() != null && player.GetKitchenObject() == null){
            GetKitchenObject().SetParent(player);
            Idle();
        } else if (GetKitchenObject() == null && player.GetKitchenObject() != null){
            StoveKitchenObjectRecipeSO recipe = GetRecipe(player.GetKitchenObject().GetKitchenScriptableObject());
            if (recipe != null){
                if (recipe.state == StoveState.COOKING){
                    state = recipe.state;
                    player.GetKitchenObject().SetParent(this);
                    currentRecipe = recipe;
                    elapsedCookTime = 0;
                }
            }
        }
    }
    
    private void Idle() {
        state = StoveState.IDLE;
        elapsedCookTime = 0;
    }

    private StoveKitchenObjectRecipeSO GetRecipe(KitchenObjectSO input) {
        foreach (var ko in recipes){
            if (ko.input.objectName == input.objectName){
                return ko;
            }
        }
        return null;
    }

}
