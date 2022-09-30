using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class MapManager
    {
        private StateManager stateManager;
        private GameObject mapTransition;

        public Map Map { get; private set; }

        public MapManager(StateManager stateManager, Map startingMap)
        {
            this.stateManager = stateManager;
            Map = GameObject.Instantiate(startingMap);
            mapTransition = Resources.Load<GameObject>("Transitions/MapTransition");
        }

        public void LoadMap(Map newMap, int destinationId)
        {
            if (stateManager.TryState(GameState.MapChange))
                Game.Player.StartCoroutine(Co_LoadMap(newMap, destinationId));
        }

        private IEnumerator Co_LoadMap(Map newMap, int destinationId)
        {
            Animator animator = FadeOut();
            while (animator.IsAnimating())
                yield return null;

            SwapMaps(newMap);
            LocatePlayerOnNewMap(destinationId);

            animator.Play("MapTransition_FadeIn");
            yield return null;
            while (animator.IsAnimating())
                yield return null;

            GameObject.Destroy(animator.gameObject);

            stateManager.RestorePreviousState();
        }

        private Animator FadeOut()
        {
            Vector3 position = Game.Player.transform.position;
            Animator animator = GameObject.Instantiate(mapTransition, position, Quaternion.identity).GetComponent<Animator>();
            return animator;
        }

        private void SwapMaps(Map newMap)
        {
            Map oldMap = Map;
            Map = GameObject.Instantiate(newMap);
            GameObject.Destroy(oldMap.gameObject);
        }

        private void LocatePlayerOnNewMap(int destinationId)
        {
            Transfer[] transfers = GameObject.FindObjectsOfType<Transfer>();
            Transfer transfer = transfers.Where(transfer => transfer.Id == destinationId).ToList().FirstOrDefault();
            Game.Player.transform.position = (transfer.Cell + transfer.Offset).Center2D();
        }  
    }
}
