using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Battle
{
    public class BattleSetup
    {
        private List<Actor> actors;
        private List<Ally> allies;
        private List<Enemy> enemies;
        private TurnBar turnBar;

        public BattleSetup(List<Actor> actors, List<Ally> allies, List<Enemy> enemies, TurnBar turnBar)
        {
            this.actors = actors;
            this.allies = allies;
            this.enemies = enemies;
            this.turnBar = turnBar;
        }

        public void PerformSetup()
        {
            SpawnPartyMembers();
            PositionPartyMembers();
            SpawnEnemies();
            SetupTurnbar();
        }

        private void SpawnPartyMembers()
        {
            foreach (PartyMember member in Party.ActiveMembers)
            {
                GameObject partyMember = ScriptableObject.Instantiate(member.ActorPrefab, new Vector2(-10, 0), Quaternion.identity);
                Ally ally = partyMember.GetComponent<Ally>();
                ally.Stats = member.Stats;
                actors.Add(ally);
                allies.Add(ally);
            }
        }

        private void PositionPartyMembers()
        {
            int partyCount = Party.ActiveMembers.Count;
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

            foreach (Ally ally in allies)
            {
                ally.transform.position = spawnPositions[spawnPositionIndex];
                spawnPositionIndex++;
            }
        }

        private void SpawnEnemies()
        {
            EnemyPack pack = BattleControl.EnemyPack;

            for (int i = 0; i < pack.Enemies.Count; i++)
            {
                Vector2 spawnPosition = new Vector2(pack.XSpawnCoordinates[i], pack.YSpawnCoordinates[i]);
                EnemyData enemyData = ScriptableObject.Instantiate(pack.Enemies[i]);
                Enemy enemy = GameObject.Instantiate(enemyData.ActorPrefab, spawnPosition, Quaternion.identity).GetComponent<Enemy>();
                enemy.Stats = enemyData.Stats;
                actors.Add(enemy);
                enemies.Add(enemy);
            }
        }

        private void SetupTurnbar()
        {
            turnBar.SpawnPortraitSlots(actors);
            turnBar.SpawnActorPortraits();
        }
    }
}
