using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    public const string OPEN_CLOSE = "OpenClose";

    [SerializeField]
    private ContainerCounter containerCounter;

    private Animator animator;

    public void Awake() {
        containerCounter.OnOpenContainerCounter += AnimateContainerOpen;
    }
    public void Start() {
        animator = GetComponent<Animator>();
    }

    private void AnimateContainerOpen(object sender, EventArgs e) {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
