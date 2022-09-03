using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    private Player player;
    private Command command;

    private enum Command
    {
        None,
        MoveLeft,
        MoveRight,
        MoveUp,
        MoveDown,
        Interact,
        ToggleMenu,
    }

    public InputHandler(Player player)
    {
        this.player = player;
    }

    public void CheckInput()
    {
        command = Command.None;

        if (Game.State == GameState.Cutscene)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            command = Command.ToggleMenu;
            HandleCommand(command);
            return;
        }

        if (Game.State != GameState.World)
            return;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            command = Command.MoveLeft;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            command = Command.MoveRight;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            command = Command.MoveUp;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            command = Command.MoveDown;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            command = Command.Interact;
        }

        if (command != Command.None)
        {
            HandleCommand(command);
        }
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

        player.Move.TryMove(direction);
    }

    private void ProcessInteract()
    {
        Vector2Int cellToCheck = player.Facing + Game.Map.Grid.GetCell2D(player.gameObject);

        if (!Game.Map.OccupiedCells.ContainsKey(cellToCheck))
        {
            return;
        }

        if (Game.Map.OccupiedCells[cellToCheck] is IInteractable interactable)
        {
            interactable.Interact();
        }
    }

    private void ProcessToggleMenu()
    {
        Game.ToggleMenu();
    }
}
