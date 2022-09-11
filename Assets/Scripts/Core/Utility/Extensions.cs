using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public static class Extensions
    {
        public static Vector2Int GetCell2D(this Grid grid, GameObject gameObject)
        {
            Vector3 position = gameObject.transform.position;
            return (Vector2Int)grid.WorldToCell(position);
        }

        public static Vector2 GetCellCenter2D(this Grid grid, Vector2Int cell)
        {
            Vector3Int threeDimensionCell = new Vector3Int(cell.x, cell.y, 0);

            return (Vector2)grid.GetCellCenterWorld(threeDimensionCell);
        }

        public static bool IsAnimating(this Animator animator, int layer = 0)
        {
            bool result = (animator.GetCurrentAnimatorStateInfo(layer).normalizedTime < 1);
            return result;
        }

        public static bool IsBasic(this Vector2Int direction)
        {
            if (direction == Direction.Up || direction == Direction.Down || direction == Direction.Left || direction == Direction.Right)
            {
                return true;
            }

            return false;
        }

        public static int IndexOf<T>(this IReadOnlyList<T> list, T item)
        {
            int i = 0;
            foreach(T element in list)
            {
                if (Equals(element, item))
                    return i;

                i++;
            }

            return -1;
        }
    }
}
