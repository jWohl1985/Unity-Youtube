using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Core;

namespace Battle
{
    public class BattleControl : MonoBehaviour
    {
        public static EnemyPack EnemyPack;

        private TurnBar turnBar;
        private List<Actor> turnOrder = new List<Actor>();
        private List<Ally> allies = new List<Ally>();
        private List<Enemy> enemies = new List<Enemy>();

        public IReadOnlyList<Actor> TurnOrder => turnOrder;
        public IReadOnlyList<Ally> Allies => allies;
        public IReadOnlyList<Enemy> Enemies => enemies;
        public bool SetupComplete { get; private set; }
        public int TurnNumber { get; private set; }

        private void Awake()
        {
            turnBar = GameObject.FindObjectOfType<TurnBar>();
            BattleSetup setup = new BattleSetup(turnOrder, allies, enemies, turnBar);
            setup.PerformSetup();
        }

        private void Update()
        {
            if (!SetupComplete)
            {
                foreach (Enemy enemy in enemies)
                    enemy.WasDefeated += OnDeath;
                DetermineTurnOrder();
                turnOrder[0].StartTurn();
                SetupComplete = true;
            }

            if (turnOrder[TurnNumber].IsTakingTurn)
                return;

            else
            {
                CheckForEnd();
                GoToNextTurn();
            }
        }

        private void DetermineTurnOrder() => turnOrder = turnOrder.OrderByDescending(actor => actor.Stats.Initiative).ToList(); 

        private void CheckForEnd()
        {
            if (enemies.Count == 0)
            {
                Game.Battle.EndBattle();
            }
        }

        private void GoToNextTurn()
        {
            TurnNumber = (TurnNumber + 1) % turnOrder.Count;
            turnOrder[TurnNumber].StartTurn();
        }

        private void OnDeath()
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i].Stats.HP == 0)
                {
                    enemies[i].WasDefeated -= OnDeath;
                    int index = turnOrder.IndexOf(enemies[i]);
                    if (index <= TurnNumber)
                        TurnNumber--;
                    turnOrder.Remove(enemies[i]);
                    enemies.Remove(enemies[i]);
                }
            }
        }
    }
}
