using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class BattlePortrait : MonoBehaviour
    {
        private RectTransform rectTransform;
        private TurnBar turnBar;
        private GameObject parent;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            turnBar = FindObjectOfType<TurnBar>();
            foreach(KeyValuePair<GameObject,bool> kvp in turnBar.Slots)
            {
                if (kvp.Value == false)
                {
                    parent = kvp.Key;
                    this.transform.parent = parent.transform;
                    turnBar.Slots[kvp.Key] = true;
                    break;
                }
            }
        }

        private void Update()
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, new Vector2(0, 0), .2f);
        }
    }
}
