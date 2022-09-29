using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class CutsceneManager
    {
        private StateManager stateManager;

        public CutsceneManager(StateManager stateManager)
        {
            this.stateManager = stateManager;
        }

        public bool TryPlayCutscene(Cutscene scene)
        {
            if (Game.State != GameState.World)
                return false;

            stateManager.SetState(GameState.Cutscene);
            foreach (ICutsceneCommand command in scene.Commands)
                command.IsFinished = false;
            Game.Player.StartCoroutine(Co_PlayCutscene(scene));
            return true;
        }

        private IEnumerator Co_PlayCutscene(Cutscene scene)
        {
            foreach (ICutsceneCommand command in scene.Commands)
            {
                Game.Player.StartCoroutine(command.Co_Execute());

                while (command.IsFinished == false)
                    yield return null;
            }

            scene.HasBeenPlayed = true;
            stateManager.SetState(GameState.World);
        }
    }
}
