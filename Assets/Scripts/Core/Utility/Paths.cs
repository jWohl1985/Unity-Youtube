using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public static class Paths
    {
        public static string enemyDataPath = "ScriptableObjects/EnemyData/";
        public static string enemyPackPath = "ScriptableObjects/EnemyPacks/";
        public static string partyMemberPath = "ScriptableObjects/PartyMembers/";

        // enemy data
        public static string Goblin = enemyDataPath + "Goblin";

        // enemy packs
        public static string TwoGoblin = enemyPackPath + "TwoGoblin";

        // party members
        public static string BlackWraith = partyMemberPath + "BlackWraith";
        public static string Satyr = partyMemberPath + "Satyr";
        public static string Wraith = partyMemberPath + "Wraith";
        public static string Minotaur = partyMemberPath + "Minotaur";

        // other
        public static string BattleTransition = "BattleTransition";
    }
}
