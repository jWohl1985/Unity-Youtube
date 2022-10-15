using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class CommandFetcher
    {
        private BattleControl battleControl;
        private TargetSystem targetSystem;
        private Ally ally;

        public IBattleCommand Command { get; private set; }

        public CommandFetcher(Ally ally)
        {
            this.ally = ally;
            battleControl = GameObject.FindObjectOfType<BattleControl>();
            targetSystem = battleControl.GetComponentInChildren<TargetSystem>();
        }

        public IEnumerator Co_GetCommand()
        {
            while (Command == null)
            {
                // TEST CODE
                if (Input.GetKeyDown(KeyCode.A))
                {
                    targetSystem.GetTarget(TargetType.AnySingle, TargetDefault.Enemy);

                    while (targetSystem.IsFindingTarget)
                        yield return null;

                    Actor target = targetSystem.SelectedTargets[0];
                    Command = new Attack(ally, target);
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    Command = new RunAway(ally);
                }
                // TEST CODE

                yield return null;
            }
        }
    }
}
