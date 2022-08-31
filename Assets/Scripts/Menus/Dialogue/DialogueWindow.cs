using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueWindow : MonoBehaviour
{
    [SerializeField] private DialogueScene scene;
    [SerializeField] private Image portrait;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI message;

    private int currentSceneDialogueIndex = 0;

    private void Start()
    {
        DisplayDialogue(scene.Dialogues[currentSceneDialogueIndex]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentSceneDialogueIndex++;
            if (currentSceneDialogueIndex < scene.Dialogues.Count)
            {
                DisplayDialogue(scene.Dialogues[currentSceneDialogueIndex]);
            }
            else
            {
                Debug.Log("Conversation over");
            }
        }
    }

    private void DisplayDialogue(Dialogue dialogue)
    {
        portrait.sprite = dialogue.Sprite;
        name.text = dialogue.Name;
        message.text = dialogue.Message;
    }
}
