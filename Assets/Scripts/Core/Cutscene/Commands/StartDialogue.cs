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

        public bool IsFinished { get; set; }

        public IEnumerator Co_Execute()
        {
            Game.Dialogue.StartDialogue(dialogueLines);

            while (Game.State == GameState.Dialogue)
            {
                yield return null;
            }

            IsFinished = true;
        }

        public override string ToString() => "Start Dialogue";
    }
}
