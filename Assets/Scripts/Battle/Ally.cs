using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class Ally : Actor
    {
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

            StartCoroutine(Co_GetPlayerCommand());
        }

        private IEnumerator Co_GetPlayerCommand()
        {
            CommandFetcher commandFetcher = new CommandFetcher(this);
            StartCoroutine(commandFetcher.Co_GetCommand());

            while (commandFetcher.Command is null)
                yield return null;

            IBattleCommand command = commandFetcher.Command;

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
    }
}
