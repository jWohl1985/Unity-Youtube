using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public static class BattleCalculations
    {
        public static int AttackDamage(Actor attacker, Actor defender)
        {
            int damage = Math.Max(0, attacker.Stats.STR - defender.Stats.ARM);
            defender.TakeDamage(damage);
            return damage;
        }
    }
}
