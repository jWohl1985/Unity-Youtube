using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public interface ICutsceneCommand
    {
        IEnumerator Co_Execute();
        bool IsFinished { get; set; }
    }
}
