using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class StateManager
    {
        private Stack<GameState> stateHistory = new Stack<GameState>();

        public GameState State { get; private set; }

       
        public bool TryState(GameState state)
        {
            bool success;

            success = state switch
            {
                GameState.Battle => CanBattle(),
                GameState.Cutscene => CanCutscene(),
                GameState.Dialogue => CanDialogue(),
                GameState.MapChange => CanMapChange(),
                GameState.Menu => CanMenu(),
                GameState.World => CanWorld(),
                _ => false,
            };

            if (success == false)
            {
                Debug.LogWarning($"State change to {state} rejected!");
                return false;
            }

            stateHistory.Push(State);
            State = state;
            return true;
        }

        public void RestorePreviousState()
        {
            State = stateHistory.Pop();
        }

        private bool CanBattle()
        {
            return State switch
            {
                GameState.Battle => false,
                GameState.Cutscene => true,
                GameState.Dialogue => false,
                GameState.MapChange => false,
                GameState.Menu => false,
                GameState.World => true,
                _ => false,
            };
        }

        private bool CanCutscene()
        {
            return State switch
            {
                GameState.Battle => false,
                GameState.Cutscene => true,
                GameState.Dialogue => true,
                GameState.MapChange => false,
                GameState.Menu => false,
                GameState.World => true,
                _ => false,
            };
        }

        private bool CanDialogue()
        {
            return State switch
            {
                GameState.Battle => false,
                GameState.Cutscene => true,
                GameState.Dialogue => false,
                GameState.MapChange => false,
                GameState.Menu => false,
                GameState.World => true,
                _ => false,
            };
        }

        private bool CanMapChange()
        {
            return State switch
            {
                GameState.Battle => false,
                GameState.Cutscene => true,
                GameState.Dialogue => false,
                GameState.MapChange => false,
                GameState.Menu => false,
                GameState.World => true,
                _ => false,
            };
        }

        private bool CanMenu()
        {
            return State switch
            {
                GameState.Battle => false,
                GameState.Cutscene => false,
                GameState.Dialogue => false,
                GameState.MapChange => false,
                GameState.Menu => false,
                GameState.World => true,
                _ => false,
            };
        }

        private bool CanWorld()
        {
            return State switch
            {
                GameState.Battle => true,
                GameState.Cutscene => true,
                GameState.Dialogue => true,
                GameState.MapChange => true,
                GameState.Menu => true,
                GameState.World => false,
                _ => true,
            };
        }

    }
}
