using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

[CreateAssetMenu(fileName = "New Dialogue Scene", menuName = "New Dialogue Scene")]
public class Dialogue : ScriptableObject
{
    [SerializeField] private List<DialogueLine> dialogues;

    public IReadOnlyList<DialogueLine> DialogueLines => dialogues;
}
