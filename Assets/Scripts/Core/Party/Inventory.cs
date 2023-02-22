using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Inventory
    {
        private Weapon dagger = Resources.Load<Weapon>("ScriptableObjects/InventoryItems/Equipment/Weapons/Dagger");
        private Armor shield = Resources.Load<Armor>("ScriptableObjects/InventoryItems/Equipment/Armors/Shield");
        private Accessory luckyCharm = Resources.Load<Accessory>("ScriptableObjects/InventoryItems/Equipment/Accessories/LuckyCharm");

        private UsableItem lifePotion = Resources.Load<UsableItem>("ScriptableObjects/InventoryItems/UsableItems/Life Potion");


        private Dictionary<InventoryItem, int> items = new Dictionary<InventoryItem, int>();

        public IReadOnlyDictionary<InventoryItem, int> Items => items;

        public void Initialize()
        {
            items.Add(dagger, 1);
            items.Add(shield, 1);
            items.Add(luckyCharm, 1);

            items.Add(lifePotion, 1);
        }

        public void AddItem(InventoryItem item, int quantity=1)
        {
            if (items.ContainsKey(item))
            {
                items[item] += quantity;
            }

            items.Add(item, quantity);

            items[item] = Mathf.Clamp(items[item], 0, 99);
        }

        public void RemoveItem(InventoryItem item, int quantity = 1)
        {
            if (!items.ContainsKey(item))
            {
                return;
            }

            items[item] -= quantity;
            items[item] = Mathf.Clamp(items[item], 0, 99);
        }
    }
}
