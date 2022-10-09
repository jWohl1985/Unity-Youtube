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
                foreach (Actor actor in turnOrder)
                    SubscribeToActorEvents(actor);
                DetermineTurnOrder();
                turnOrder[0].StartTurn();
                SetupComplete = true;
            }

            if (TurnNumber < 0)
            {
                CheckForEnd();
                turnOrder[0].StartTurn();
                TurnNumber = 0;
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
            if (allies.Count == 0)
            {
                Game.Battle.EndBattle();
            }
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

        private void SubscribeToActorEvents(Actor actor)
        {
            actor.WasDefeated += OnDeath;
            actor.Escaped += OnEscape;
        }

        private void UnsubscribeFromActorEvents(Actor actor)
        {
            actor.WasDefeated -= OnDeath;
            actor.Escaped -= OnEscape;
        }

        private void OnDeath(Actor actor)
        {
            if (actor is Enemy enemy)
            {
                UnsubscribeFromActorEvents(enemy);
                RemoveFromBattle(enemy);
            }
        }

        private void OnEscape(Actor actor)
        {
            UnsubscribeFromActorEvents(actor);
            RemoveFromBattle(actor);
        }

        private void RemoveFromBattle(Actor actor)
        {
            int index = turnOrder.IndexOf(actor);
            if (index <= TurnNumber)
                TurnNumber--;
            turnOrder.Remove(actor);

            if (actor is Ally)
                allies.Remove(actor as Ally);
            else
                enemies.Remove(actor as Enemy);
        }
    }
}
