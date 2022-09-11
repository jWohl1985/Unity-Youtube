using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class BattlePortrait : MonoBehaviour
    {
        private static int currentSlotIndex = 0;

        private BattleControl battleControl;
        private RectTransform rectTransform;
        private TurnBar turnBar;
        private Actor actor;

        private void Awake()
        {
            battleControl = FindObjectOfType<BattleControl>();
            rectTransform = GetComponent<RectTransform>();
            turnBar = FindObjectOfType<TurnBar>();

            rectTransform.SetParent(turnBar.Slots[currentSlotIndex].transform, false);
            actor = battleControl.TurnOrder[currentSlotIndex];
            currentSlotIndex++;
        }

        private void Update()
        {
            rectTransform.SetParent(turnBar.Slots[actor.TurnNumber].transform, false);
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, new Vector2(0, 0), .2f);
        }
    }
}
