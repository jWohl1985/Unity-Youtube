using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public enum BattleCommand
    {
        Attack,
        Special,
        Run,
        Item,
    }

    public enum TargetType
    {
        AnySingle,
        SingleAlly,
        SingleEnemy,
        GroupAlly,
        GroupEnemy,
        GroupAll,
    }

    public enum TargetDefault
    {
        Ally,
        Enemy,
    }
}
