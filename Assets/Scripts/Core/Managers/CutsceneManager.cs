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
            if (stateManager.TryState(GameState.Cutscene) == false)
                return false;

            foreach (ICutsceneCommand command in scene.Commands)
                command.IsFinished = false;

            scene.StartCoroutine(Co_PlayCutscene(scene));
            return true;
        }

        private IEnumerator Co_PlayCutscene(Cutscene scene)
        {
            foreach (ICutsceneCommand command in scene.Commands)
            {
                scene.StartCoroutine(command.Co_Execute());

                while (command.IsFinished == false)
                    yield return null;
            }

            scene.HasBeenPlayed = true;
            stateManager.RestorePreviousState();
        }
    }
}
