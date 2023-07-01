using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField]
    private CutterCounter cutterCounter;
    // Start is called before the first frame update
    void Start(){
        cutterCounter.OnActiveUI += OnActiveUI;
    }
    private void OnActiveUI(object sender, CutterCounter.OnActiveUIArgs e) {
        gameObject.SetActive(e.active);
    }



}
