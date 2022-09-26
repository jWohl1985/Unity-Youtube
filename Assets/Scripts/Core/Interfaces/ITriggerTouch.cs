using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public interface ITriggerTouch
    {
        Vector2Int Cell { get; }

        void Trigger();
    }
}
