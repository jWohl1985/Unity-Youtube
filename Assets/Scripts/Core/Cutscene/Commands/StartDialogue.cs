using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class StartDialogue : ICutsceneCommand
    {
        [SerializeField] private Dialogue dialogueToStart;

        public bool IsFinished { get; private set; }

        public IEnumerator Co_Execute()
        {
            Game.Manager.StartDialogue(dialogueToStart);

            while (Game.Manager.State == GameState.Dialogue)
            {
                yield return null;
            }

            IsFinished = true;
        }

        public override string ToString() => "Start Dialogue";
    }
}
