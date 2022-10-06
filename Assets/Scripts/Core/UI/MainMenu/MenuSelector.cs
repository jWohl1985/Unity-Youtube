using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class MenuSelector : MonoBehaviour
    {
        private MainMenu mainMenu;
        private RectTransform rectTransform;
        private List<RectTransform> selectableOptions = new List<RectTransform>();

        public int SelectedIndex { get; set; } = 0;

        public IReadOnlyList<RectTransform> SelectableOptions => selectableOptions;

        private void Awake()
        {
            mainMenu = GetComponentInParent<MainMenu>();
            rectTransform = GetComponent<RectTransform>();

            for (int i = 0; i < rectTransform.parent.childCount; i++)
            {
                if (rectTransform.parent.GetChild(i).CompareTag("Selectable"))
                    selectableOptions.Add(rectTransform.parent.GetChild(i).GetComponent<RectTransform>());
            }
        }


        void Update()
        {
            if (mainMenu.CurrentSelector != this)
                return;

            if (rectTransform.anchoredPosition != selectableOptions[SelectedIndex].anchoredPosition)
                MoveToSelectedOption();
        }

        private void MoveToSelectedOption()
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, selectableOptions[SelectedIndex].anchoredPosition, 8f);
        }
    }
}
