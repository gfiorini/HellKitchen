using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangeArgs> OnProgressChange;

    public event EventHandler<OnStateChangedArgs> OnStateChanged;
    public class OnStateChangedArgs : EventArgs
    {
        public StoveState state;
    }
    
    public enum StoveState {
        IDLE,
        COOKING,
        OVERCOOKING
    }
    
    
    
    
    [SerializeField]
    private StoveKitchenObjectRecipeSO[] recipes;

    private float elapsedCookTime = 0f;
    
    private float overCookTime = 0f;

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
            case StoveState.IDLE:
                Idle();
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
            OnStateChanged?.Invoke(this, new OnStateChangedArgs{state = state});
            elapsedCookTime = 0;
            overCookTime = 0;
            IHasProgress.OnProgressChangeArgs p = new IHasProgress.OnProgressChangeArgs();
            p.progressNormalized = 1;
            OnProgressChange?.Invoke(this,p);
        } else{
            IHasProgress.OnProgressChangeArgs p = new IHasProgress.OnProgressChangeArgs();
            p.progressNormalized = elapsedCookTime / currentRecipe.timer;
            OnProgressChange?.Invoke(this,p);
        }
    }

    private void OverCooking() {
        overCookTime += Time.deltaTime;
        if (overCookTime > currentRecipe.timer){
            GetKitchenObject().DestroySelf();
            AssignKitchenObject(currentRecipe.output, this);
            OnProgressChange?.Invoke(this,new IHasProgress.OnProgressChangeArgs());     
            currentRecipe = null;
            state = StoveState.IDLE;
            OnStateChanged?.Invoke(this, new OnStateChangedArgs{state = state});
        } else{
            IHasProgress.OnProgressChangeArgs p = new IHasProgress.OnProgressChangeArgs();
            p.progressNormalized = overCookTime / currentRecipe.timer;
            OnProgressChange?.Invoke(this,p);            
        }  
    }

    public override void Interact(Player player) {
        if (GetKitchenObject() != null && player.GetKitchenObject() == null) {
            GetKitchenObject().SetParent(player);
            ShutDown();
            Idle();
        }
        else if (GetKitchenObject() == null && player.GetKitchenObject() != null){
            StoveKitchenObjectRecipeSO recipe = GetRecipe(player.GetKitchenObject().GetKitchenScriptableObject());
            if (recipe != null){
                if (recipe.state == StoveState.COOKING){
                    state = recipe.state;
                    OnStateChanged?.Invoke(this, new OnStateChangedArgs{state = state});
                    player.GetKitchenObject().SetParent(this);
                    currentRecipe = recipe;
                    elapsedCookTime = 0;
                }
            }
        } else if (GetKitchenObject() != null && player.GetKitchenObject() != null){
            if (player.GetKitchenObject().TryGetPlate(out Plate plate)){
                if (plate.TryAddIngredient(GetKitchenObject().GetKitchenScriptableObject())){
                    ShutDown();
                    Idle();
                    GetKitchenObject().DestroySelf();
                    
                }
            }
        }
    }

    private void ShutDown()
    {
        IHasProgress.OnProgressChangeArgs p = new IHasProgress.OnProgressChangeArgs();
        p.progressNormalized = 0;
        OnProgressChange?.Invoke(this, p);
    }

    private void Idle() {
        state = StoveState.IDLE;
        OnStateChanged?.Invoke(this, new OnStateChangedArgs{state = state});
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


