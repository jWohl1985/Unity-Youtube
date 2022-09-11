using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class Attack : ICommand
    {
        private Actor attacker;
        private List<Actor> targets;
        private float moveSpeed = .01f;

        public bool IsFinished { get; private set; } = false;

        public Attack(Actor actor, List<Actor> targets)
        {
            this.attacker = actor;
            this.targets = targets;
        }

        public IEnumerator Co_Execute()
        {
            while(attacker.transform.position != targets[0].transform.position)
            {
                attacker.transform.position = Vector2.MoveTowards(attacker.transform.position, targets[0].transform.position, moveSpeed);
                yield return null;
            }

            IsFinished = true;
        }
    }
}
