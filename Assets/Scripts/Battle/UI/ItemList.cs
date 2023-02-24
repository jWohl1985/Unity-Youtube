using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class ItemList : MonoBehaviour
{
    [SerializeField] private GameObject itemInfoPrefab;
    [SerializeField] private GameObject listOfItems;

    private void Start()
    {
        foreach(InventoryItem item in Party.Inventory.Items.Keys)
        {
            if (item is UsableItem usableItem)
            {
                ItemInfo itemInfo = Instantiate(itemInfoPrefab, listOfItems.transform).GetComponent<ItemInfo>();
                itemInfo.SetItem(usableItem);
            }
        }
    }
}
