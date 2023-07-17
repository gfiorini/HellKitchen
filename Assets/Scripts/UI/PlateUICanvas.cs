using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateUICanvas : MonoBehaviour
{
    [SerializeField]
    private Plate plate;

    [SerializeField]
    private Transform iconTemplate;

    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }
    void Start() {
        plate.OnAddIngredient += PlateOnOnAddIngredient;
    }
    private void PlateOnOnAddIngredient(object sender, Plate.IngredientEventArgs e) {
        Transform icon = Instantiate(iconTemplate, transform);
        IconTemplate template = icon.GetComponent<IconTemplate>();
        template.SetIcon(e.ingredient);
        icon.gameObject.SetActive(true);
    }
    
}
