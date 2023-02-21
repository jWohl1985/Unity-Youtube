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


        private List<Equipment> equipment = new List<Equipment>();
        private List<UsableItem> usableItems = new List<UsableItem>();

        public IReadOnlyList<Equipment> Equipment => equipment;
        public IReadOnlyList<UsableItem> UsableItems => usableItems;

        public void Initialize()
        {
            equipment.Add(dagger);
            equipment.Add(shield);
            equipment.Add(luckyCharm);

            usableItems.Add(lifePotion);

            foreach (var item in usableItems)
            {
                Debug.Log(item.ItemName);
            }
        }
    }
}
