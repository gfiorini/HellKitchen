using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    public enum CameraMode
    {
        LookAt,
        InverseLookAt,
        Forward
    }

    [SerializeField]
    private CameraMode _cameraMode;
    
    private void LateUpdate() {
        switch (_cameraMode){
            case CameraMode.LookAt:
                transform.LookAt(Camera.main.transform);                
                break;
            case CameraMode.InverseLookAt:
                //gfiorini: capire
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position; 
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case CameraMode.Forward:
                break;            
        }
        
    }
}
