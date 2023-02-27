using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class ItemList : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private GameObject itemInfoPrefab;
    [SerializeField] private GameObject listOfItems;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        foreach (InventoryItem item in Party.Inventory.Items.Keys)
        {
            if (item is UsableItem usableItem)
            {
                ItemInfo itemInfo = Instantiate(itemInfoPrefab, listOfItems.transform).GetComponent<ItemInfo>();
                itemInfo.SetItem(usableItem);
            }
        }
        animator.Play("Open");
    }

    public void Close()
    {
        foreach (RectTransform child in listOfItems.GetComponent<RectTransform>())
        {
            Destroy(child.gameObject);
        }

        animator.Play("Close");
    }
}
