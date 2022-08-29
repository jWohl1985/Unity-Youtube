using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Direction
{
    public static readonly Vector2Int Up = new Vector2Int(0, 1);
    public static readonly Vector2Int Down = new Vector2Int(0, -1);
    public static readonly Vector2Int Left = new Vector2Int(-1, 0);
    public static readonly Vector2Int Right = new Vector2Int(1, 0);

    public static bool IsBasic(this Vector2Int direction)
    {
        if (direction == Direction.Up || direction == Direction.Down || direction == Direction.Left || direction == Direction.Right)
        {
            return true;
        }

        return false;
    }

    public static Vector2 Center2D(this Vector2Int cell)
    {
        Vector3Int threeDimensionCell = new Vector3Int(cell.x, cell.y, 0);

        return (Vector2)Game.Map.Grid.GetCellCenterWorld(threeDimensionCell);
    }
}
