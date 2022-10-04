using System.Collections;
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

        public void OpenMenu()
        {
            if (stateManager.TryState(GameState.Menu))
            {
                mainMenu.Open();
                mainMenu.StartCoroutine(Co_WaitForMenu());
            }
        }

        private IEnumerator Co_WaitForMenu()
        {
            while (mainMenu.IsOpen)
                yield return null;

            stateManager.RestorePreviousState();
        }
    }
}
