using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Inventory
    {
        private Weapon dagger = Resources.Load<Weapon>("ScriptableObjects/Equipment/Weapons/Dagger");
        private Armor shield = Resources.Load<Armor>("ScriptableObjects/Equipment/Armors/Shield");
        private Accessory luckyCharm = Resources.Load<Accessory>("ScriptableObjects/Equipment/Accessories/LuckyCharm");


        private List<Equipment> equipment = new List<Equipment>();

        public IReadOnlyList<Equipment> Equipment => equipment;

        public void Initialize()
        {
            equipment.Add(dagger);
            equipment.Add(shield);
            equipment.Add(luckyCharm);
        }
    }
}
