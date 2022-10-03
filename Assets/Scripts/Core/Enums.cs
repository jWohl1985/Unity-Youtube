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
        MapChange,
        Dialogue,
        Cutscene,
        Battle,
        Menu,
    }
    public enum TriggerType
    {
        Autoplay,
        Touch,
        Call,
    }

    public enum EquipmentEffect
    {
        None,
        IncreasedLoot,
        IncreaseMaxHP,
    }

    public enum Job
    {
        Fighter,
        BlackMage,
        WhiteMage,
        Thief,
    }
}
