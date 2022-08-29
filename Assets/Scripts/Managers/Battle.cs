using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public static EnemyPack EnemyPack;

    private List<Actor> turnOrder = new List<Actor>();
    private int turnNumber = 0;
    private bool setupComplete = false;

    public IReadOnlyList<Actor> TurnOrder => turnOrder;

    private void Awake()
    {
        SpawnPartyMembers();
        SpawnEnemies(); 
    }

    private void Update()
    {
        if (!setupComplete)
        {
            DetermineTurnOrderAndStartFirstTurn();
        }

        if (turnOrder[turnNumber].IsTakingTurn)
            return;

        else
        {
            CheckForEnd();
            GoToNextTurn();
        }
    }

    private void SpawnPartyMembers()
    {
        Vector2 spawnPosition = new Vector2(-5, -2);
        foreach(PartyMember member in Party.ActiveMembers)
        {
            GameObject partyMember = Instantiate(member.ActorPrefab, spawnPosition, Quaternion.identity);
            Ally ally = partyMember.GetComponent<Ally>();
            ally.Stats = member.Stats;
            turnOrder.Add(ally);
            spawnPosition.y += 2;          
        }
    }

    private void SpawnEnemies()
    {
        for(int i = 0; i < EnemyPack.Enemies.Count; i++)
        {
            Vector2 spawnPosition = new Vector2(EnemyPack.XSpawnCoordinates[i], EnemyPack.YSpawnCoordinates[i]);
            GameObject enemyActor = Instantiate(EnemyPack.Enemies[i].ActorPrefab, spawnPosition, Quaternion.identity);
            Enemy enemy = enemyActor.GetComponent<Enemy>();
            enemy.Stats = EnemyPack.Enemies[i].Stats;
            turnOrder.Add(enemy);
        }
    }

    private void DetermineTurnOrderAndStartFirstTurn()
    {
        turnOrder = turnOrder.OrderByDescending(actor => actor.Stats.Initiative).ToList();
        turnOrder[0].StartTurn();
        setupComplete = true;
    }

    private void CheckForEnd()
    {
        // to be implemented
    }

    private void GoToNextTurn()
    {
        turnNumber = (turnNumber + 1) % turnOrder.Count;
        turnOrder[turnNumber].StartTurn();
    }
}
