using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Transfer : MonoBehaviour
    {
        private Map currentMap;
        private Vector2Int exitCell;
        
        [SerializeField] private int id;
        [SerializeField] private Map newMap;
        [SerializeField] private int destinationId;

        public int Id => id;
        public Vector2Int Cell => exitCell;
     
        private void Awake()
        {
            currentMap = FindObjectOfType<Map>();
            exitCell = currentMap.Grid.GetCell2D(this.gameObject);
        }

        private void Start()
        {
            currentMap.Transfers.Add(exitCell, this);
        }

        public void TeleportPlayer() => Game.Manager.LoadMap(newMap, destinationId);
    }
}
