using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridExtensions
{
    public static Vector2Int GetCell2D(this Grid grid, GameObject gameObject)
    {
        Vector3 position = gameObject.transform.position;
        return (Vector2Int) grid.WorldToCell(position);
    }

    public static Vector2 GetCellCenter2D(this Grid grid, Vector2Int cell)
    {
        Vector3Int threeDimensionCell = new Vector3Int(cell.x, cell.y, 0);

        return (Vector2) grid.GetCellCenterWorld(threeDimensionCell);
    }
}
