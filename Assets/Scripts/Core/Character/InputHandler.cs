using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class InputHandler
    {
        private Player player;
        private Command command;
        private Map map => Game.World.Map;

        private enum Command
        {
            None,
            MoveLeft,
            MoveRight,
            MoveUp,
            MoveDown,
            Interact,
            ToggleMenu,
            AdvanceDialogue,
        }

        public InputHandler(Player player)
        {
            this.player = player;
        }

        public void CheckInput()
        {
            command = Command.None;

            switch (Game.State)
            {
                case (GameState.Battle):
                case (GameState.MapChange):
                default:
                    break;
                case (GameState.Menu):
                    if (Input.GetKeyDown(KeyCode.Escape))
                        command = Command.ToggleMenu;
                    break;
                case (GameState.Dialogue):
                    if (Input.GetKeyDown(KeyCode.Space))
                        command = Command.AdvanceDialogue;
                    break;
                case (GameState.World):
                    if (Input.GetKeyDown(KeyCode.Escape))
                        command = Command.ToggleMenu;

                    else if (Input.GetKey(KeyCode.LeftArrow))
                        command = Command.MoveLeft;

                    else if (Input.GetKey(KeyCode.RightArrow))
                        command = Command.MoveRight;

                    else if (Input.GetKey(KeyCode.UpArrow))
                        command = Command.MoveUp;

                    else if (Input.GetKey(KeyCode.DownArrow))
                        command = Command.MoveDown;

                    else if (Input.GetKeyDown(KeyCode.Space))
                        command = Command.Interact;
                    break;
            }

            if (command != Command.None)
                HandleCommand(command);
        }

        private void HandleCommand(Command command)
        {
            switch (command)
            {
                case (Command.MoveLeft):
                case (Command.MoveRight):
                case (Command.MoveUp):
                case (Command.MoveDown):
                    ProcessMovement(command);
                    break;
                case (Command.Interact):
                    ProcessInteract();
                    break;
                case (Command.ToggleMenu):
                    ProcessToggleMenu();
                    break;
                case (Command.AdvanceDialogue):
                    ProcessAdvanceDialogue();
                    break;
            }
        }

        private void ProcessMovement(Command command)
        {
            Vector2Int direction = new Vector2Int(0, 0);

            switch (command)
            {
                case (Command.MoveLeft):
                    direction = Direction.Left;
                    break;
                case (Command.MoveRight):
                    direction = Direction.Right;
                    break;
                case (Command.MoveUp):
                    direction = Direction.Up;
                    break;
                case (Command.MoveDown):
                    direction = Direction.Down;
                    break;
            }

            player.Movement.TryMove(direction);
        }

        private void ProcessInteract()
        {
            Vector2Int cellToCheck = player.Facing + player.CurrentCell;

            if (!map.OccupiedCells.ContainsKey(cellToCheck))
            {
                return;
            }

            if (map.OccupiedCells[cellToCheck] is IInteractable interactable)
            {
                interactable.Interact();
            }
        }

        private void ProcessToggleMenu() => Game.Menu.ToggleMenu();

        private void ProcessAdvanceDialogue() => Game.Dialogue.AdvanceDialogue();
    }
}
