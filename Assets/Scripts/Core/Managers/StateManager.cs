using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class StateManager
    {
        public GameState State { get; private set; }
        public GameState PreviousState { get; private set; }

        public bool SetState(GameState state)
        {
            PreviousState = State;
            State = state;
            return true;
        }
    }
}
