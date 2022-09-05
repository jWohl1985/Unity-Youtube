using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Map : MonoBehaviour
    {
        public Dictionary<Vector2Int, MonoBehaviour> OccupiedCells { get; private set; }
        public Dictionary<Vector2Int, Transfer> Exits { get; private set; }
        public Grid Grid { get; private set; }

        private void Awake()
        {
            Grid = GetComponent<Grid>();
            OccupiedCells = new Dictionary<Vector2Int, MonoBehaviour>();
            Exits = new Dictionary<Vector2Int, Transfer>();
            OccupiedCells.Clear();
        }
    }
}
