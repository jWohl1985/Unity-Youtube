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
        private CommandMenu commandMenu;

        public IBattleCommand Command { get; private set; }

        public CommandFetcher(Ally ally)
        {
            this.ally = ally;
            battleControl = GameObject.FindObjectOfType<BattleControl>();
            targetSystem = battleControl.GetComponentInChildren<TargetSystem>();
            commandMenu = GameObject.FindObjectOfType<CommandMenu>();
        }

        public IEnumerator Co_GetCommand()
        {
            commandMenu.Activate();

            while (commandMenu.SelectedCommand is null)
            {
                yield return null;
            }

            commandMenu.Deactivate();

            switch (commandMenu.SelectedCommand)
            {
                case (BattleCommand.Attack):
                    targetSystem.GetTarget(TargetType.AnySingle, TargetDefault.Enemy);

                    while (targetSystem.IsFindingTarget)
                        yield return null;

                    Actor target = targetSystem.SelectedTargets[0];
                    Command = new Attack(ally, target);
                    break;

                case (BattleCommand.Run):
                    Command = new RunAway(ally);
                    break;
            }
        }
    }
}
