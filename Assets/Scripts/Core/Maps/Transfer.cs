using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Transfer : MonoBehaviour, ITriggerTouch
    {
        private Map currentMap;
        private Vector2Int exitCell;
        
        [SerializeField] private int id;
        [SerializeField] private Map newMap;
        [SerializeField] private int destinationId;
        [SerializeField] private Vector2Int offset;

        public int Id => id;
        public Vector2Int Cell => exitCell;
        public Vector2Int Offset => offset;
     
        private void Awake()
        {
            currentMap = FindObjectOfType<Map>();
            exitCell = currentMap.Grid.GetCell2D(this.gameObject);
        }

        private void Start()
        {
            currentMap.TriggerCells.Add(exitCell, this);
        }

        public void Trigger() => Game.Manager.LoadMap(newMap, destinationId);
    }
}
