using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class CommandFetcher
    {
        private BattleControl battleControl;
        private Actor actor;

        public ICommand Command { get; private set; }

        public CommandFetcher(Actor actor)
        {
            this.actor = actor;
            battleControl = GameObject.FindObjectOfType<BattleControl>();
        }

        public IEnumerator Co_GetCommand()
        {
            while (Command == null)
            {
                // TEST CODE
                if (Input.GetKeyDown(KeyCode.A))
                {
                    Command = new Attack(actor, battleControl.Enemies[0]);
                }
                // TEST CODE

                yield return null;
            }
        }
    }
}
