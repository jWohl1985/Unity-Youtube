using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    ScriptableObject Interaction { get; }
    void Interact();
}
