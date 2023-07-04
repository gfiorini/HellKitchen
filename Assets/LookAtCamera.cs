using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    private Transform cameraTransform;
    public enum CameraMode
    {
        LookAt,
        InverseLookAt,
        Forward,
        Inverse
    }

    [SerializeField]
    private CameraMode _cameraMode;

    private void Start() {
        cameraTransform = Camera.main.transform;
    }
    private void LateUpdate() {
        switch (_cameraMode){
            case CameraMode.LookAt:
                transform.LookAt(cameraTransform);                
                break;
            case CameraMode.InverseLookAt:
                // Calculate the direction from the object to the camera
                Vector3 lookDirection = transform.position - cameraTransform.position;
                // Rotate the object to look in the opposite direction of the camera
                transform.LookAt(transform.position + lookDirection);
                break;
            case CameraMode.Forward:
                transform.forward = cameraTransform.forward;
                break;
            case CameraMode.Inverse:
                transform.forward = -cameraTransform.forward;
                break;                        
        }
        
    }
}
