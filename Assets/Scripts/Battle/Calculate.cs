using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public static class Calculate
    {
        public static int AttackDamage(Actor attacker, Actor defender)
        {
            int damage = attacker.Stats.STR - defender.Stats.ARM;
            defender.Stats.HP -= damage;
            return damage;
        }
    }
}
