using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItem : InventoryItem
{
    [SerializeField] private int itemlevel;
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] private bool canUseInMenu;
    [SerializeField] private bool canUseInBattle;

    public int ItemLevel
    {
        get => itemlevel;
        set => itemlevel = Mathf.Clamp(value, 1, 99);
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

    public bool CanUseInMenu
    {
        get => canUseInMenu;
        set => canUseInMenu = value;
    }

    public bool CanUseInBattle
    {
        get => canUseInBattle;
        set => canUseInBattle = value;
    }
}
