using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Map : MonoBehaviour
    {
        public Dictionary<Vector2Int, MonoBehaviour> OccupiedCells { get; private set; } = new Dictionary<Vector2Int, MonoBehaviour>();
        public Grid Grid { get; private set; }

        private void Awake()
        {
            Grid = GetComponent<Grid>();
            OccupiedCells.Clear();
        }
    }
}
