using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    Interaction Interaction { get; }
    void Interact();
}
