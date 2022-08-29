using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Interaction", menuName = "New Interaction")]
public class Interaction : ScriptableObject
{
    public virtual void StartInteraction()
    {
        Debug.Log("Interaction successful!");
    }
}
