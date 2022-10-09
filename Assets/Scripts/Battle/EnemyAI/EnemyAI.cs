using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public abstract class EnemyAI : MonoBehaviour
    {
        protected Actor actor;
        protected BattleControl battleControl;

        protected virtual void Awake()
        {
            battleControl = FindObjectOfType<BattleControl>();
            actor = GetComponent<Actor>();
        }

        public abstract IBattleCommand ChooseAction();
    }
}
