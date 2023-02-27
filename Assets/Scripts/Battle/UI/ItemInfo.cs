using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Core;
using UnityEngine.UI;
using Battle;

public class ItemInfo : MonoBehaviour
{
    public IBattleCommand Command { get; private set; }

    private UsableItem item;

    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemQuantityAndName;

    public void SetItem(UsableItem item)
    {
        this.item = item;

        itemIcon = item.ItemIcon;
        itemQuantityAndName.text = $"({Party.Inventory.Items[item].ToString()}) {item.ItemName}";
    }

    public void UseItem()
    {
        BattleControl battleControl = GameObject.FindObjectOfType<BattleControl>();
        Ally allyUsingItem = (Ally)battleControl.TurnOrder[battleControl.TurnNumber];

        GameObject.FindObjectOfType<ItemList>().Close();
        CommandFetcher.CurrentFetcher.SetCommand(new UseItem(allyUsingItem, item));
    }
}
