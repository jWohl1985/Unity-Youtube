using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTurner
{
    public Vector2Int Facing { get; private set; } = Direction.Down;

    public void Turn(Vector2Int direction)
    {
        if (direction.IsBasic())
        {
            Facing = direction;
        }
    }

    public void TurnAround() => Facing = new Vector2Int(-Facing.x, -Facing.y);
}
