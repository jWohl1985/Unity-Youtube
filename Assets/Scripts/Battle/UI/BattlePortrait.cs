using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class BattlePortrait : MonoBehaviour
    {
        private static int currentSlotIndex = 0;

        private RectTransform rectTransform;
        private TurnBar turnBar;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            turnBar = FindObjectOfType<TurnBar>();
            rectTransform.SetParent(turnBar.Slots[currentSlotIndex].transform, false);
            currentSlotIndex++;
        }

        private void Update()
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, new Vector2(0, 0), .2f);
        }
    }
}
