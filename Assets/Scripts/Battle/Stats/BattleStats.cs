using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public abstract class BattleStats
    {
        public static readonly int MAXIMUM_POSSIBLE_LEVEL = 99;
        public static readonly int MAXIMUM_POSSIBLE_HP = 999;
        public static readonly int MAXIMUM_POSSIBLE_STR = 99;
        public static readonly int MAXIMUM_POSSIBLE_ARM = 99;
        public static readonly int MAXIMUM_POSSIBLE_SPD = 99;

        public abstract int Level { get; }
        public abstract int HP { get; }
        public abstract int MaxHP { get; }
        public abstract int STR { get; }
        public abstract int ARM { get; }
        public abstract int SPD { get; }
        public int Initiative => SPD + Random.Range(-1, 2);

        public abstract void ReduceHP(int amount);
    }
}
