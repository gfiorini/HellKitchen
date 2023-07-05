using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
 public class KitchenObjectRecipeSO : ScriptableObject
 {
     public KitchenObjectSO input;
     
     public KitchenObjectSO output;

     public int numCuts;

 }
