using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public static EnemyPack EnemyPack;

    private List<Actor> turnOrder = new List<Actor>();
    private List<Ally> allies = new List<Ally>();
    private List<Enemy> enemies = new List<Enemy>();
    private int turnNumber = 0;
    private bool setupComplete = false;

    public IReadOnlyList<Actor> TurnOrder => turnOrder;
    public IReadOnlyList<Ally> Allies => Allies;
    public IReadOnlyList<Enemy> Enemies => Enemies;


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
        int partyCount = Party.ActiveMembers.Count;

        foreach(PartyMember member in Party.ActiveMembers)
        {
            GameObject partyMember = Instantiate(member.ActorPrefab, new Vector2(-10, 0), Quaternion.identity);
            Ally ally = partyMember.GetComponent<Ally>();
            ally.Stats = member.Stats;
            turnOrder.Add(ally);
            allies.Add(ally);
        }

        List<Vector2> spawnPositions = new List<Vector2>();

            switch (partyCount)
            {
                case 1:
                    spawnPositions.Add(new Vector2(-3, 0));
                    break;
                case 2:
                    spawnPositions.Add(new Vector2(-3, 1));
                    spawnPositions.Add(new Vector2(-3, -1));
                    break;
                case 3:
                    spawnPositions.Add(new Vector2(-3.5f, 1));
                    spawnPositions.Add(new Vector2(-3, -.5f));
                    spawnPositions.Add(new Vector2(-3.5f, -2));
                    break;
                case 4:
                    spawnPositions.Add(new Vector2(-3.3f, 1.65f));
                    spawnPositions.Add(new Vector2(-3.8f, 0.4f));
                    spawnPositions.Add(new Vector2(-3.3f, -1.1f));
                    spawnPositions.Add(new Vector2(-3.8f, -2.6f));
                    break;
            }

        int spawnPositionIndex = 0;

        foreach(Ally ally in allies)
        {
            ally.transform.position = spawnPositions[spawnPositionIndex];
            spawnPositionIndex++;
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
            enemies.Add(enemy);
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
