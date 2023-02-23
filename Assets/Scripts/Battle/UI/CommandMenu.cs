using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battle
{
    public class CommandMenu : MonoBehaviour
    {
        private RectTransform rect;
        private bool isActive;

        private float activeYposition;
        private float inactiveYposition;
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private GameObject defaultSelection;

        private float currentYPosition => rect.anchoredPosition.y;

        public BattleCommand? SelectedCommand { get; private set; } = null;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();

            activeYposition = rect.anchoredPosition.y + 300f;
            inactiveYposition = rect.anchoredPosition.y;
        }

        public void Update()
        {
            if (isActive && currentYPosition != activeYposition)
            {
                rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, new Vector2(rect.anchoredPosition.x, activeYposition), moveSpeed);
            }

            if (!isActive && currentYPosition != inactiveYposition)
            {
                rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, new Vector2(rect.anchoredPosition.x, inactiveYposition), moveSpeed);
            }
        }


        public void Activate()
        {
            isActive = true;
            EventSystem.current.SetSelectedGameObject(defaultSelection);
            SelectedCommand = null;
        }

        public void Deactivate()
        {
            isActive = false;
        }

        public void Attack()
        {
            SelectedCommand = BattleCommand.Attack;
        }

        public void Run()
        {
            SelectedCommand = BattleCommand.Run;
        }

        public void Item()
        {
            SelectedCommand = BattleCommand.Item;
        }
    }
}
