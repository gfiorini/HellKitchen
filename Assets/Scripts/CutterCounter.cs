using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterCounter  : ClearCounter
{

    public event EventHandler<OnProgressChangeArgs> OnProgressChange;
    
    public event EventHandler<OnActiveUIArgs> OnActiveUI;

    public class OnProgressChangeArgs : EventArgs
    {
        public float progressNormalized;
    }

    public class OnActiveUIArgs : EventArgs
    {
        public bool active;
    }
    
    private int currentNumCuts;
    
    [SerializeField]
    private CutKitchenObjectRecipeSO[] cutKitchenObjectRecipeSO;

    private void Start() {
        ResetCutterCounter();
        DoActiveUI(false);        
    }

    public override void Interact(Player player) {
        if (GetKitchenObject() != null && player.GetKitchenObject() == null){
            GetKitchenObject().SetParent(player);
            DoActiveUI(false);
        } else if (GetKitchenObject() == null && player.GetKitchenObject() != null && ExistRecipeForKitchenObject(player.GetKitchenObject())){
            ResetCutterCounter();
            DoActiveUI(true);
            player.GetKitchenObject().SetParent(this);
        }
    }
    private void ResetCutterCounter() {
        currentNumCuts = 0;
        UpdateProgressBar(currentNumCuts, 1);
    }

    private void DoActiveUI(bool active) {
        OnActiveUIArgs args = new OnActiveUIArgs();
        args.active = active;
        OnActiveUI?.Invoke(this, args);
    }

    public override void AlternateInteract(Player player) {
        KitchenObject inputKitchenObject = GetKitchenObject();
        if (inputKitchenObject != null && player.GetKitchenObject() == null){
            CutKitchenObjectRecipeSO cutRecipe = GetCutRecipeSo(inputKitchenObject.GetKitchenScriptableObject());
            if (cutRecipe != null){
                currentNumCuts++;
                float maxCuts = cutRecipe.numCuts;
                UpdateProgressBar(currentNumCuts, maxCuts);
                if (currentNumCuts >= maxCuts){
                    inputKitchenObject.DestroySelf();
                    AssignKitchenObject(cutRecipe.output, this);
                    DoActiveUI(false);
                }
            }
        }
    }
    private void UpdateProgressBar(float numCuts, float maxCuts) {
        OnProgressChangeArgs args = new OnProgressChangeArgs();
        args.progressNormalized = numCuts/ maxCuts;
        OnProgressChange?.Invoke(this, args);
    }

    private CutKitchenObjectRecipeSO GetCutRecipeSo(KitchenObjectSO input) {
        foreach (var ko in cutKitchenObjectRecipeSO){
            if (ko.input.objectName == input.objectName){
                return ko;
            }
        }
        return null;
    }

    private bool ExistRecipeForKitchenObject(KitchenObject currentKitchenObject) {
        return GetCutRecipeSo(currentKitchenObject.GetKitchenScriptableObject()) != null;
    }

}
