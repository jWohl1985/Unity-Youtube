using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class TurnBar : MonoBehaviour
    {
        [SerializeField] private GameObject portraitSlotPrefab;

        public Dictionary<GameObject, bool> Slots = new Dictionary<GameObject, bool>();

        public void SpawnPortraitSlots(List<Actor> actors)
        {
            foreach(Actor actor in actors)
            {
                GameObject slot = Instantiate(portraitSlotPrefab, this.gameObject.transform);
                Slots.Add(slot, false);
            }
        }
    }
}
