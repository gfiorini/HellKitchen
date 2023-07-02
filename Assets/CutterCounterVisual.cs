using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut";

    [SerializeField]
    private CutterCounter _cutterCounter;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();        
    }

    private void Start() {
        _cutterCounter.OnCut += OnCut;
    }
    private void OnCut(object sender, EventArgs e) {
        animator.SetTrigger(CUT); 
    }
}
