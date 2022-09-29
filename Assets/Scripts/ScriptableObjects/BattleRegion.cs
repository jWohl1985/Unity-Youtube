using System.Collections;
using System.Collections.Generic;
using Battle;
using UnityEngine;
using Core;

[CreateAssetMenu(fileName = "New Region", menuName = "New Region")]
public class BattleRegion : ScriptableObject
{
    [SerializeField] private int encounterChancePerStep;
    [SerializeField] private List<EnemyPack> enemyPacks;
    [SerializeField] private List<int> packEncounterRate;

    public int EncounterChancePerStep => encounterChancePerStep;

    public EnemyPack GetRandomEnemyPack()
    {
        int random = Random.Range(1, 101);

        for (int i = 0; i < enemyPacks.Count; i++)
        {
            if (random <= packEncounterRate[i])
                return enemyPacks[i];
        }

        Debug.LogWarning("Couldn't choose enemy pack!");
        return enemyPacks[0];
    }

    public void CheckForEncounter(Map map)
    {
        int random = Random.Range(1, 101);

        if (random <= map.Region.EncounterChancePerStep)
        {
            EnemyPack enemyPack = GetRandomEnemyPack();
            Game.Battle.StartBattle(enemyPack);
        }
    }
}
