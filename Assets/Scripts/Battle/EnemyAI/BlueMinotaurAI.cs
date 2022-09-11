using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class BlueMinotaurAI : EnemyAI
    {
        private Actor actor;

        private void Awake()
        {
            actor = GetComponent<Actor>();
        }

        public override ICommand ChooseAction()
        {
            List<Actor> target = new List<Actor>();
            target.Add(FindObjectOfType<Ally>());
            return new Attack(actor, target);
        }
    }
}
