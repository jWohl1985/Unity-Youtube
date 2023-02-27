using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class UseItem : IBattleCommand
    {
        private Ally allyUsingItem;
        private UsableItem item;

        public bool IsFinished { get; private set; } = false;

        public UseItem(Ally allyUsingItem, UsableItem item)
        {
            this.allyUsingItem = allyUsingItem;
            this.item = item;
        }

        public IEnumerator Co_Execute()
        {
            Debug.Log($"{allyUsingItem} is using {item.ItemName}");
            Party.Inventory.RemoveItem(item);

            yield return null;
            IsFinished = true;
        }
    }
}
