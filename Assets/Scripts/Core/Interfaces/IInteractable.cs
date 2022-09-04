using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public interface IInteractable
    {
        ScriptableObject Interaction { get; }
        void Interact();
    }
}
