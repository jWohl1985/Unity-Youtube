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

            foreach(GameObject slot in turnBar.Slots)
            {
                if (slot.GetComponentInChildren<BattlePortrait>() == null)
                {
                    rectTransform.SetParent(slot.transform, false);
                    int index = slot.transform.GetSiblingIndex() - 1;
                    actor = battleControl.TurnOrder[index];
                    break;
                }
            }
        }

        private void Update()
        {
            rectTransform.SetParent(turnBar.Slots[actor.TurnNumber].transform, false);
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, new Vector2(0, 0), .2f);
        }
    }
}
