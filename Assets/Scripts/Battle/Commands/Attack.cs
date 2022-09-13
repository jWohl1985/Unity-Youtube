using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class Attack : ICommand
    {
        private Actor attacker;
        private Actor defender;
        private Transform attackerTransform;
        private Transform defenderTransform;
        private float moveSpeed = .01f;

        private readonly Vector3 attackOffset = new Vector3(1.3f, 0, 0);

        public bool IsFinished { get; private set; } = false;

        public Attack(Actor attacker, Actor defender)
        {
            this.attacker = attacker;
            this.defender = defender;
            this.attackerTransform = attacker.GetComponent<Transform>();
            this.defenderTransform = defender.GetComponent<Transform>();
        }

        public IEnumerator Co_Execute()
        {
            Vector3 targetPosition = defenderTransform.position + attackOffset;

            attacker.Animator.Play("Moving");
            while(attackerTransform.position != targetPosition)
            {
                attackerTransform.position = Vector3.MoveTowards(attackerTransform.position, targetPosition, moveSpeed);
                yield return null;
            }

            attacker.Animator.Play("Attack");
            while(attacker.Animator.IsAnimating())
                yield return null;
            attacker.Animator.Play("Idle");

            Calculate.AttackDamage(attacker, defender);

            IsFinished = true;
        }
    }
}
