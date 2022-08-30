using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueWindow : MonoBehaviour
{
    [SerializeField] private Image portrait;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI message;

    public void SetWindowProperties(Sprite sprite, string speakerName, string speakerMessage)
    {
        portrait.sprite = sprite;
        name.text = speakerName;
        message.text = speakerMessage;
    }
}
