using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public enum Dir
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3,
    }

    public enum GameState
    {
        World,
        Loading,
        Cutscene,
        Battle,
        Menu,
    }

    public enum TransitionType
    {
        Battle,
        MapChange,
    }
}
