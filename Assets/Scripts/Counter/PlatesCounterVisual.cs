using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{

    [SerializeField]
    private PlatesCounter platesCounter;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private Transform prefab;

    private List<Transform> plates;

    private float offset = 0.1f;


    private void Awake(){
        plates = new List<Transform>();
    }

    private void Start()
    {
        platesCounter.OnPlateSpawn += PlatesCounter_OnPlateSpawn;
        platesCounter.OnPlatePickup += PlatesCounter_OnPlatePickup;
    }

    private void PlatesCounter_OnPlatePickup(object sender, EventArgs e){
        Transform plate = plates[plates.Count - 1];
        plates.Remove(plate);
        Destroy(plate.gameObject);
    }

    private void PlatesCounter_OnPlateSpawn(object sender, System.EventArgs e){
        Transform t = Instantiate(prefab, spawnPoint);
        plates.Add(t);
        t.localPosition = new Vector3(0, offset * (plates.Count - 1), 0);
    }


}
