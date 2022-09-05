using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Exit : MonoBehaviour
    {
        private Map currentMap;
        private Vector2Int exitCell;

        [SerializeField] private Map newMap;
        [SerializeField] private Vector2Int destinationCell;

        private void Awake()
        {
            currentMap = FindObjectOfType<Map>();
            exitCell = currentMap.Grid.GetCell2D(this.gameObject);
        }

        private void Start()
        {
            currentMap.Exits.Add(exitCell, this);
        }

        public void TeleportPlayer()
        {
            Game.Manager.LoadMap(newMap, destinationCell);
        }
    }
}
