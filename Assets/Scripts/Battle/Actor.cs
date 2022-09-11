using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Battle
{
    public abstract class Actor : MonoBehaviour
    {
        protected BattleControl battleControl;
        protected Vector2 startingPosition;
        protected Vector2 battlePosition = new Vector2(0.5f, 0);

        public bool IsTakingTurn { get; protected set; } = false;
        public BattleStats Stats { get; set; }
        public int TurnNumber => battleControl.TurnOrder.IndexOf(this);


        protected void Awake()
        {
            battleControl = FindObjectOfType<BattleControl>();
        }

        protected virtual void Start()
        {
            startingPosition = transform.position;
        }

        public abstract void StartTurn();
    }
}
