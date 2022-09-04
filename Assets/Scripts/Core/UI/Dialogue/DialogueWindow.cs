using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Core
{
    public class DialogueWindow : MonoBehaviour
    {
        [SerializeField] private Dialogue dialogue;
        [SerializeField] private Image portrait;
        [SerializeField] private TextMeshProUGUI characterName;
        [SerializeField] private TextMeshProUGUI message;

        private Animator animator;
        private string dialogueOpenAnimation = "DialogueOpen";
        private string dialogueCloseAnimation = "DialogueClose";
        private int currentDialogueLine = 0;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GoToNextLine();
            }
        }

        public void Open(Dialogue dialogueToPlay)
        {
            dialogue = dialogueToPlay;
            currentDialogueLine = 0;
            ShowDialogueLine(dialogue.DialogueLines[currentDialogueLine]);
            animator.Play(dialogueOpenAnimation);
        }

        public void GoToNextLine()
        {
            currentDialogueLine++;
            if (currentDialogueLine < dialogue.DialogueLines.Count)
            {
                ShowDialogueLine(dialogue.DialogueLines[currentDialogueLine]);
            }
            else
            {
                Close();
            }
        }

        private void ShowDialogueLine(DialogueLine dialogue)
        {
            portrait.sprite = dialogue.Sprite;
            characterName.text = dialogue.Name;
            message.text = dialogue.Message;
        }

        private void Close()
        {
            animator.Play(dialogueCloseAnimation);
            Game.Manager.EndDialogue();
        }
    }
}
