using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Battle
{
    [Serializable]
    public class EnemyStats : BattleStats
    {
        [SerializeField] private int level;
        [SerializeField] private int hp;
        [SerializeField] private int maxhp;
        [SerializeField] private int str;
        [SerializeField] private int arm;
        [SerializeField] private int spd;

        public override int Level => level;
        public override int HP => hp;
        public override int MaxHP => maxhp;
        public override int STR => str;
        public override int ARM => arm;
        public override int SPD => spd;

        public override void ReduceHP(int amount)
        {
            if (amount <= 0)
                return;

            hp = Mathf.Clamp(hp - amount, 0, maxhp);
        }
    }
}
