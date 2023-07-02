using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterCounter  : ClearCounter
{

    public event EventHandler<OnProgressChangeArgs> OnProgressChange;

    public event EventHandler OnCut;

    public class OnProgressChangeArgs : EventArgs
    {
        public float progressNormalized;
    }

    private int currentNumCuts = 0;
    
    [SerializeField]
    private CutKitchenObjectRecipeSO[] cutKitchenObjectRecipeSO;

    public override void Interact(Player player) {
        if (GetKitchenObject() != null && player.GetKitchenObject() == null){
            GetKitchenObject().SetParent(player);
            ResetCuttingCounter(1);       
        } else if (GetKitchenObject() == null && player.GetKitchenObject() != null && ExistRecipeForKitchenObject(player.GetKitchenObject())){
            player.GetKitchenObject().SetParent(this);
            ResetCuttingCounter(0);
        }
         
    }
    private void ResetCuttingCounter(float progress) {
        currentNumCuts = 0;
        UpdateProgressBar(progress);
    }

    public override void AlternateInteract(Player player) {
        KitchenObject inputKitchenObject = GetKitchenObject();
        if (inputKitchenObject != null && player.GetKitchenObject() == null){
            CutKitchenObjectRecipeSO cutRecipe = GetCutRecipeSo(inputKitchenObject.GetKitchenScriptableObject());
            if (cutRecipe != null){
                currentNumCuts++;
                int maxCuts = cutRecipe.numCuts;
                float progressNormalized = (float)currentNumCuts / maxCuts;
                UpdateProgressBar(progressNormalized);
                OnCut?.Invoke(this, EventArgs.Empty);;
                if (currentNumCuts >= maxCuts){
                    inputKitchenObject.DestroySelf();
                    AssignKitchenObject(cutRecipe.output, this);
                }
            }
        }
    }
    private void UpdateProgressBar(float progressNormalized) {
        OnProgressChangeArgs args = new OnProgressChangeArgs();
        args.progressNormalized = progressNormalized;
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
