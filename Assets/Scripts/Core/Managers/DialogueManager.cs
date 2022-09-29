using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class DialogueManager
    {
        private StateManager stateManager;
        private DialogueWindow dialogueWindow;

        public DialogueManager(StateManager stateManager)
        {
            this.stateManager = stateManager;
            this.dialogueWindow = GameObject.FindObjectOfType<DialogueWindow>();
        }

        public void StartDialogue(List<DialogueLine> sceneToPlay)
        {
            stateManager.SetState(GameState.Dialogue);
            dialogueWindow.Open(sceneToPlay);
        }

        public void AdvanceDialogue()
        {
            if (!dialogueWindow.IsOpen || Game.State != GameState.Dialogue || dialogueWindow.IsAnimating)
                return;

            dialogueWindow.GoToNextLine();
        }

        public void EndDialogue() => stateManager.SetState(stateManager.PreviousState);
    }
}
