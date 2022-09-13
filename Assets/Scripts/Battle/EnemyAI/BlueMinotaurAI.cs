using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class BlueMinotaurAI : EnemyAI
    {
        public override ICommand ChooseAction()
        {
            Actor defender = GetRandomTarget();
            return new Attack(actor, defender);
        }

        private Actor GetRandomTarget()
        {
            return battleControl.Allies[Random.Range(0, battleControl.Allies.Count)];
        }
    }
}
