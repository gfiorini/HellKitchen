using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
 public class StoveKitchenObjectRecipeSO : ScriptableObject
 {
     public KitchenObjectSO input;
     
     public KitchenObjectSO output;

     public float timer;

     public StoveCounter.StoveState state = StoveCounter.StoveState.IDLE;

 }
