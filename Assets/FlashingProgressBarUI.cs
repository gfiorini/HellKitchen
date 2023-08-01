using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingProgressBarUI : MonoBehaviour
{
    private const string IS_FLASHING = "IsFlashing";

    [SerializeField]
    private StoveCounter _stoveCounter;

    private Animator _animator;
    void Start() {
        _animator = GetComponent<Animator>();
        _stoveCounter.OnProgressChange += StoveCounterOnProgresssdChange;
        _animator.SetBool(IS_FLASHING, false);
    }
    private void StoveCounterOnProgresssdChange(object sender, IHasProgress.OnProgressChangeArgs e) {
        bool flashing = _stoveCounter.GetState() == StoveCounter.StoveState.OVERCOOKING && e.progressNormalized > 0.5f;
        _animator.SetBool(IS_FLASHING, flashing);
    }



}
