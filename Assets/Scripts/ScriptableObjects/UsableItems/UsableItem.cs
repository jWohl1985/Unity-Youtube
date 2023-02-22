using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItem : InventoryItem
{
    [SerializeField] private bool canUseInMenu;
    [SerializeField] private bool canUseInBattle;

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
