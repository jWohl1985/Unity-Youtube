using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Battle
{
    public class TurnBar : MonoBehaviour
    {
        [SerializeField] private GameObject portraitSlotPrefab;

        public List<GameObject> Slots = new List<GameObject>();

        public void SpawnPortraitSlots(List<Actor> actors)
        {
            foreach(Actor actor in actors)
            {
                GameObject slot = Instantiate(portraitSlotPrefab, this.gameObject.transform);
                Slots.Add(slot);
            }
        }

        public void SpawnActorPortraits()
        {
            SpawnPartyMemberPortraits();
            SpawnEnemyPortraits();
        }

        private void SpawnPartyMemberPortraits()
        {
            foreach(PartyMember member in Party.ActiveMembers)
            {
                Instantiate(member.BattlePortrait, this.transform);
            }
        }

        private void SpawnEnemyPortraits()
        {
            foreach(EnemyData enemyData in BattleControl.EnemyPack.Enemies)
            {
                Instantiate(enemyData.BattlePortrait, this.transform);
            }
        }
    }
}
