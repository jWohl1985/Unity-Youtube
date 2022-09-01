using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTurner
{
    private Character character;

    public Vector2Int Facing { get; private set; } = Direction.Down;

    public CharacterTurner(Character character)
    {
        this.character = character;
    }

    public void Turn(Vector2Int direction)
    {
        if (direction.IsBasic())
        {
            Facing = direction;
        }
    }
    public void TurnAround() => Facing = new Vector2Int(-Facing.x, -Facing.y);

    public void TurnToPlayer()
    {
        Player player = Game.Player;

        if(player.CurrentCell.x > character.CurrentCell.x)
        {
            Turn(Direction.Right);
        }
        else if (player.CurrentCell.x < character.CurrentCell.x)
        {
            Turn(Direction.Left);
        }
        else if (player.CurrentCell.y > character.CurrentCell.y)
        {
            Turn(Direction.Up);
        }
        else
        {
            Turn(Direction.Down);
        }
    }

    
}
