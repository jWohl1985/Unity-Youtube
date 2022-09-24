using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class StartDialogue : ICutsceneCommand
    {
        [SerializeField] private List<DialogueLine> dialogueLines;

        public bool IsFinished { get; private set; }

        public IEnumerator Co_Execute()
        {
            Game.Manager.StartDialogue(dialogueLines);

            while (Game.Manager.State == GameState.Dialogue)
            {
                yield return null;
            }

            IsFinished = true;
        }

        public override string ToString() => "Start Dialogue";
    }
}
