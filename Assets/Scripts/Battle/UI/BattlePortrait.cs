using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class BattlePortrait : MonoBehaviour
    {
        private BattleControl battleControl;
        private RectTransform rectTransform;
        private TurnBar turnBar;
        private Actor actor;

        private void Awake()
        {
            battleControl = FindObjectOfType<BattleControl>();
            rectTransform = GetComponent<RectTransform>();
            turnBar = FindObjectOfType<TurnBar>();

            foreach(RectTransform slot in turnBar.Slots)
            {
                if (slot.GetComponentInChildren<BattlePortrait>() == null)
                {
                    rectTransform.SetParent(slot, false);
                    int index = slot.GetSiblingIndex() - 1;
                    actor = battleControl.TurnOrder[index];
                    if (actor is Enemy enemy)
                        enemy.WasDefeated += RemovePortrait;
                    break;
                }
            }
        }

        private void Start()
        {
            rectTransform.SetParent(turnBar.transform, false);
        }

        private void Update()
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, turnBar.Slots[actor.TurnNumber].anchoredPosition, 1f);
        }

        private void RemovePortrait()
        {
            (actor as Enemy).WasDefeated -= RemovePortrait;
            RectTransform slot = turnBar.Slots[actor.TurnNumber];
            turnBar.Slots.Remove(slot);
            Destroy(this.gameObject);
            Destroy(slot.gameObject);
        }
    }
}
