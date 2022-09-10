using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class TurnIndicator : MonoBehaviour
    {
        private TurnBar turnBar;
        private RectTransform rectTransform;
        private BattleControl battleControl;

        private List<RectTransform> slots = new List<RectTransform>();

        private void Awake()
        {
            turnBar = GetComponentInParent<TurnBar>();
            rectTransform = GetComponent<RectTransform>();
            battleControl = FindObjectOfType<BattleControl>();
        }

        void Start()
        {
            foreach(GameObject slot in turnBar.Slots)
            {
                slots.Add(slot.GetComponent<RectTransform>());
            }
        }

        void Update()
        {
            Vector2 currentPosition = rectTransform.anchoredPosition;
            Vector2 targetPosition = slots[battleControl.TurnNumber].anchoredPosition;

            float speed = 1f;
            if (battleControl.TurnNumber == 0)
                speed = 3f;

            rectTransform.anchoredPosition = Vector2.MoveTowards(currentPosition, targetPosition + new Vector2(0,-60), speed);
        }
    }
}
