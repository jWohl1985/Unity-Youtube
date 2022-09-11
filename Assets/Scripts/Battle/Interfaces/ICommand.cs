using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public interface ICommand
    {
        bool IsFinished { get; }
        IEnumerator Co_Execute();
    }
}
