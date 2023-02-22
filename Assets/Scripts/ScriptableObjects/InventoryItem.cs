using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryItem : ScriptableObject
{
    [SerializeField] private int itemLevel;
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;

    public int ItemLevel
    {
        get => itemLevel;
        set => itemLevel = Mathf.Clamp(value, 1, 99);
    }

    public string ItemName
    {
        get => itemName;
        set => itemName = value;
    }

    public string ItemDescription
    {
        get => itemDescription;
        set => itemDescription = value;
    }
}
