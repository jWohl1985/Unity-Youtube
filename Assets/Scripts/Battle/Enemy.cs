using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class Enemy : Actor
    {
        private EnemyAI ai;

        protected override void Awake()
        {
            base.Awake();
            ai = GetComponent<EnemyAI>();
            WasDefeated += OnDeath;
        }

        public override void StartTurn()
        {
            IsTakingTurn = true;
            StartCoroutine(Co_MoveToCenter());
        }

        private IEnumerator Co_MoveToCenter()
        {
            float elapsedTime = 0;

            Animator.Play("Moving");
            while ((Vector2)transform.position != battlePosition)
            {
                transform.position = Vector2.Lerp(startingPosition, battlePosition, elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Animator.Play("Idle");
            yield return new WaitForSeconds(.5f);

            StartCoroutine(Co_EnemyChooseAction());
        }

        private IEnumerator Co_EnemyChooseAction()
        {
            IBattleCommand command = ai.ChooseAction();

            StartCoroutine(command.Co_Execute());
            while (!command.IsFinished)
                yield return null;

            if (command is not RunAway)
                StartCoroutine(Co_EndTurn());
            else
                IsTakingTurn = false;
        }

        private IEnumerator Co_EndTurn()
        {
            float elapsedTime = 0;
            Vector2 currentPosition = transform.position;

            Animator.Play("Moving");
            while ((Vector2)transform.position != battlePosition)
            {
                transform.position = Vector2.Lerp(currentPosition, battlePosition, elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            Animator.Play("Idle");
            yield return new WaitForSeconds(.5f);

            elapsedTime = 0;
            Animator.Play("Moving");
            while ((Vector2)transform.position != startingPosition)
            {
                transform.position = Vector2.Lerp(battlePosition, startingPosition, elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            Animator.Play("Idle");

            IsTakingTurn = false;
        }

        private void OnDeath(Actor actor) => StartCoroutine(Co_Die());

        private IEnumerator Co_Die()
        {
            WasDefeated -= OnDeath;
            Animator.Play("Death");
            yield return null;
            while (Animator.IsAnimating())
                yield return null;
            Destroy(this.gameObject);
        }
    }
}
