using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class MenuManager
    {
        private StateManager stateManager;
        private MainMenu mainMenu;

        public MenuManager(StateManager stateManager)
        {
            this.stateManager = stateManager;
            this.mainMenu = GameObject.FindObjectOfType<MainMenu>();
        }

        public void ToggleMenu()
        {
            if (mainMenu.IsAnimating)
                return;

            if (mainMenu.IsOpen)
            {
                if(stateManager.TryState(GameState.World))
                    mainMenu.Close();
            }
            else
            {
                if(stateManager.TryState(GameState.Menu))
                    mainMenu.Open();
            }
        }
    }
}
