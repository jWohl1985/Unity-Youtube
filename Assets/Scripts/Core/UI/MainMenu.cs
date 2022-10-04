using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private MenuSelector mainSelector;
        [SerializeField] private MenuSelector memberSelector;

        private Animator animator;
        private string menuOpenAnimation = "MenuOpen";
        private string menuCloseAnimation = "MenuClose";
        private MenuState menuState;

        private enum MenuState
        {
            Main,
            EquipMemberSelection,
        }

        public bool IsOpen { get; private set; }
        public bool IsAnimating => (animator.IsAnimating());


        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Open()
        {
            menuState = MenuState.Main;
            mainSelector.SelectedIndex = 0;
            mainSelector.IsActiveSelector = true;
            IsOpen = true;
            animator.Play(menuOpenAnimation);
        }

        public void Close()
        {
            mainSelector.IsActiveSelector = false;
            IsOpen = false;
            animator.Play(menuCloseAnimation);
        }

        public void Accept()
        {
            switch (menuState)
            {
                case (MenuState.Main):
                    ProcessMainSelection();
                    break;
            }
        }

        public void Cancel()
        {
            switch (menuState)
            {
                case (MenuState.Main):
                    Close();
                    break;
                case (MenuState.EquipMemberSelection):
                    memberSelector.IsActiveSelector = false;
                    memberSelector.SelectedIndex = 0;
                    memberSelector.gameObject.SetActive(false);
                    mainSelector.IsActiveSelector = true;
                    menuState = MenuState.Main;
                    break;     
            }
        }

        private void ProcessMainSelection()
        {
            switch (mainSelector.SelectedIndex)
            {
                case 1:
                    Equip();
                    break;
                default:
                    Debug.LogWarning("Not implemented!");
                    break;
            }
        }

        private void Equip()
        {
            menuState = MenuState.EquipMemberSelection;
            mainSelector.IsActiveSelector = false;
            memberSelector.gameObject.SetActive(true);
            memberSelector.IsActiveSelector = true;
            memberSelector.SelectedIndex = 0;
        }
    }
}
