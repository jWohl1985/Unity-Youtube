using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using System;

namespace Battle
{
    public abstract class Actor : MonoBehaviour
    {
        protected BattleControl battleControl;
        protected Vector2 startingPosition;
        protected Vector2 battlePosition = new Vector2(0.5f, 0);

        public event Action WasDefeated;

        public Animator Animator { get; protected set; }
        public bool IsTakingTurn { get; protected set; } = false;
        public BattleStats Stats { get; set; }
        public int TurnNumber => battleControl.TurnOrder.IndexOf(this);


        protected virtual void Awake()
        {
            battleControl = FindObjectOfType<BattleControl>();
            Animator = GetComponent<Animator>();
        }

        protected virtual void Start()
        {
            startingPosition = transform.position;
        }

        protected virtual void Update()
        {
            if (Stats.HP == 0)
                WasDefeated?.Invoke();
        }

        public abstract void StartTurn();
    }
}
