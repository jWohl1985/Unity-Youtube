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
            if (mainMenu.IsAnimating || Game.State == GameState.Cutscene || Game.State == GameState.Dialogue)
                return;

            if (mainMenu.IsOpen)
            {
                stateManager.SetState(GameState.World);
                mainMenu.Close();
            }
            else
            {
                stateManager.SetState(GameState.Menu);
                mainMenu.Open();
            }
        }
    }
}
