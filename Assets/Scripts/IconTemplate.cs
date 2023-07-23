using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconTemplate : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    public void SetIcon(KitchenObjectSO ko) {
        icon.sprite = ko.icon;
    }
}
