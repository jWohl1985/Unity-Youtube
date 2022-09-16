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

        private List<RectTransform> slotRects = new List<RectTransform>();

        private void Awake()
        {
            battleControl = FindObjectOfType<BattleControl>();
            rectTransform = GetComponent<RectTransform>();
            turnBar = FindObjectOfType<TurnBar>();

            foreach (GameObject slot in turnBar.Slots)
            {
                slotRects.Add(slot.GetComponent<RectTransform>());
            }

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

        private void Start()
        {
            this.gameObject.transform.SetParent(turnBar.transform, false);
        }

        private void Update()
        {
            if (actor is Enemy enemy && enemy.Stats.HP == 0)
            {
                Destroy(turnBar.Slots[actor.TurnNumber]);
                Destroy(this.gameObject);
                return;
            }
            RectTransform slotRect = slotRects[actor.TurnNumber];   
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, slotRect.anchoredPosition, .2f);
        }
    }
}
