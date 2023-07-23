using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticsManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake() {
        CutterCounter.ResetEvents();
        BaseCounter.ResetEvents();
        TrashCounter.ResetEvents();
        Plate.ResetEvents();
    }
    
}
