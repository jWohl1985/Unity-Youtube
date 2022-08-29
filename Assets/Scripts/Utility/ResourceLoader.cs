using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceLoader
{
    public static string enemyDataPath = "ScriptableObjects/EnemyData/";
    public static string enemyPackPath = "ScriptableObjects/EnemyPacks/";
    public static string partyMemberPath = "ScriptableObjects/PartyMembers/";

    // enemy data
    public static string Goblin = enemyDataPath + "Goblin";

    // enemy packs
    public static string TwoGoblin = enemyPackPath + "TwoGoblin";

    // party members
    public static string Balfam = partyMemberPath + "BalfamIrongull";
    public static string Bul = partyMemberPath + "BulSeratolva";
    public static string Enna = partyMemberPath + "EnnaEvenwind";
    public static string Maxymer = partyMemberPath + "MaxymerPyncion";

    // other
    public static string BattleTransition = "BattleTransition";

    public static T Load<T>(string resource) where T:Object
    {
        T loadedItem = Resources.Load<T>(resource);

        if (loadedItem != null)
        {
            return loadedItem;
        }
        else
        {
            Debug.LogError($"Could not locate {resource}");
            return null;
        }
    }
}
