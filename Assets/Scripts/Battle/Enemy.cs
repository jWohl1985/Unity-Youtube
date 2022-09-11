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
        }

        public override void StartTurn()
        {
            IsTakingTurn = true;
            StartCoroutine(Co_MoveToCenter());
        }

        private IEnumerator Co_MoveToCenter()
        {
            float elapsedTime = 0;

            while ((Vector2)transform.position != battlePosition)
            {
                transform.position = Vector2.Lerp(startingPosition, battlePosition, elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            StartCoroutine(Co_EnemyChooseAction());
        }

        private IEnumerator Co_EnemyChooseAction()
        {
            ICommand command;
            command = ai.ChooseAction();
            StartCoroutine(command.Co_Execute());
            while (!command.IsFinished)
            {
                yield return null;
            }
            StartCoroutine(Co_EndTurn());
        }

        private IEnumerator Co_EndTurn()
        {
            float elapsedTime = 0;
            Vector2 currentPosition = transform.position;

            while ((Vector2)transform.position != startingPosition)
            {
                transform.position = Vector2.Lerp(currentPosition, startingPosition, elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            IsTakingTurn = false;
        }
    }
}
