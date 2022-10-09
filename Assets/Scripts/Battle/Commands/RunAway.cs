using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class RunAway : IBattleCommand
    {
        private Actor actor;
        private Transform transform;
        private Animator animator;
        private SpriteRenderer spriteRenderer;

        private const float RUN_SPEED = 10f;

        public bool IsFinished { get; private set; }

        public RunAway(Actor actor)
        {
            this.actor = actor;
            this.transform = actor.GetComponent<Transform>();
            this.animator = actor.GetComponent<Animator>();
            this.spriteRenderer = actor.GetComponent<SpriteRenderer>();
        }

        public IEnumerator Co_Execute()
        {
            spriteRenderer.flipX = true;
            animator.Play("Moving");

            Vector2 targetPosition = new Vector2(-10, 0);

            while ((Vector2)transform.position != targetPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, RUN_SPEED / 1000);
                yield return null;
            }

            actor.RanAway();

            IsFinished = true;
        }
    }
}
