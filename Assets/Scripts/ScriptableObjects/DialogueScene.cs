using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Scene", menuName = "New Dialogue Scene")]
public class DialogueScene : ScriptableObject
{
    [SerializeField] private List<Dialogue> dialogues;

    public IReadOnlyList<Dialogue> Dialogues => dialogues;
}
