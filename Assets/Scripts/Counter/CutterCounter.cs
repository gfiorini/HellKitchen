using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterCounter  : ClearCounter, IHasProgress
{

    public event EventHandler<IHasProgress.OnProgressChangeArgs> OnProgressChange;
    public class OnProgressChangeArgs : EventArgs
    {
        public float progressNormalized;
    }
    public event EventHandler OnCut;

    public static EventHandler OnAnyCut;

    private int currentNumCuts = 0;
    
    [SerializeField]
    private CutterKitchenObjectRecipeSO[] cutKitchenObjectRecipeSO;

    public override void Interact(Player player) {
        if (GetKitchenObject() != null && player.GetKitchenObject() == null){
            GetKitchenObject().SetParent(player);
            ResetCuttingCounter(0);       
        } else if (GetKitchenObject() == null && player.GetKitchenObject() != null && ExistRecipeForKitchenObject(player.GetKitchenObject())){
            player.GetKitchenObject().SetParent(this);
            ResetCuttingCounter(1);
        } else if (GetKitchenObject() != null && player.GetKitchenObject() != null){
            if (player.GetKitchenObject().TryGetPlate(out Plate plate)){
                if (plate.TryAddIngredient(GetKitchenObject().GetKitchenScriptableObject())){
                    GetKitchenObject().DestroySelf();
                }
            }
        }

    }
    private void ResetCuttingCounter(float progress) {
        currentNumCuts = 0;
        UpdateProgressBar(progress);
    }

    public override void AlternateInteract(Player player) {
        KitchenObject inputKitchenObject = GetKitchenObject();
        if (inputKitchenObject != null && player.GetKitchenObject() == null){
            CutterKitchenObjectRecipeSO recipe = GetCutRecipeSo(inputKitchenObject.GetKitchenScriptableObject());
            if (recipe != null){
                currentNumCuts++;
                int maxCuts = recipe.numCuts;
                float progressNormalized = (float)currentNumCuts / maxCuts;
                UpdateProgressBar(1 - progressNormalized);
                OnCut?.Invoke(this, EventArgs.Empty);
                OnAnyCut?.Invoke(gameObject, EventArgs.Empty);
                if (currentNumCuts >= maxCuts){
                    inputKitchenObject.DestroySelf();
                    AssignKitchenObject(recipe.output, this);
                }
            }
        }
    }
    private void UpdateProgressBar(float progressNormalized) {
        IHasProgress.OnProgressChangeArgs args = new IHasProgress.OnProgressChangeArgs();
        args.progressNormalized = progressNormalized;
        OnProgressChange?.Invoke(this, args);
    }

    private CutterKitchenObjectRecipeSO GetCutRecipeSo(KitchenObjectSO input) {
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
