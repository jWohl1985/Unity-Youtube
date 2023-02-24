using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsableItem : InventoryItem
{
    [SerializeField] private bool canUseInMenu;
    [SerializeField] private bool canUseInBattle;
    [SerializeField] private Image itemIcon;

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

    public Image ItemIcon => itemIcon;
}
