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
        public static string weaponPath = "ScriptableObjects/Equipment/Weapons/";
        public static string armorPath = "ScriptableObjects/Equipment/Armors/";
        public static string accessoryPath = "ScriptableObjects/Equipment/Accessories/";

        // enemy data
        public static string Goblin = enemyDataPath + "Goblin";

        // enemy packs
        public static string TwoGoblin = enemyPackPath + "TwoGoblin";

        // party members
        public static string BlackWraith = partyMemberPath + "BlackWraith";
        public static string Satyr = partyMemberPath + "Satyr";
        public static string Wraith = partyMemberPath + "Wraith";
        public static string Minotaur = partyMemberPath + "Minotaur";

        // equipment - weapons
        public static string Dagger = weaponPath + "Dagger";
        public static string NoWeapon = weaponPath + "None";

        // equipment - armors
        public static string Shield = armorPath + "Shield";
        public static string NoArmor = armorPath + "None";

        // equipment - accessories
        public static string LuckyCharm = accessoryPath + "LuckyCharm";
        public static string NoAccessory = accessoryPath + "None";

        // other
        public static string BattleTransition = "BattleTransition";
    }
}
