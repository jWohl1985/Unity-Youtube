using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Core;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    private UsableItem item;

    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemQuantityAndName;

    public void SetItem(UsableItem item)
    {
        this.item = item;

        itemIcon = item.ItemIcon;
        itemQuantityAndName.text = $"({Party.Inventory.Items[item].ToString()}) {item.ItemName}";
    }
}
