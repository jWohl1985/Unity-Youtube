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

        [SerializeField] private AudioSource menuChangeSound;

        public bool IsActiveSelector { get; set; } = false;
        public int SelectedIndex { get; set; } = 0;


        private void Awake()
        {
            mainMenu = GetComponentInParent<MainMenu>();
            rectTransform = GetComponent<RectTransform>();

            for (int i = 0; i < rectTransform.parent.childCount; i++)
            {
                if (rectTransform.parent.GetChild(i).GetComponent<MenuSelector>() != null)
                    continue;

                selectableOptions.Add(rectTransform.parent.GetChild(i).GetComponent<RectTransform>());
            }
        }


        void Update()
        {
            if (!IsActiveSelector)
                return;

            if (rectTransform.anchoredPosition != selectableOptions[SelectedIndex].anchoredPosition)
                MoveToSelectedOption();

            if (Input.GetKeyDown(KeyCode.UpArrow) && SelectedIndex > 0)
            {
                menuChangeSound.Play();
                SelectedIndex--;
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow) && (SelectedIndex < selectableOptions.Count - 1))
            {
                menuChangeSound.Play();
                SelectedIndex++;
            }

            else if (Input.GetKeyDown(KeyCode.Return))
                Accept();

            else if (Input.GetKeyDown(KeyCode.Escape))
                Cancel();
        }

        private void MoveToSelectedOption()
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, selectableOptions[SelectedIndex].anchoredPosition, 8f);
        }

        private void Accept()
        {
            mainMenu.Accept();
        }

        private void Cancel()
        {
            mainMenu.Cancel();
        }
    }
}
