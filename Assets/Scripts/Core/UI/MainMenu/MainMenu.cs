using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private MenuSelector mainSelector;
        [SerializeField] private MenuSelector memberSelector;
        [SerializeField] private MenuSelector equipmentSelector;
        [SerializeField] private AudioSource menuChangeSound;

        private Dictionary<MenuState, MenuSelector> stateSelector = new Dictionary<MenuState, MenuSelector>();
        private MainWindow mainWindow;
        private Animator animator;
        private string menuOpenAnimation = "MenuOpen";
        private string menuCloseAnimation = "MenuClose";
        private MenuState menuState;

        private enum MenuState
        {
            Main,
            EquipMemberSelection,
            EquipmentSelection,
        }

        public bool IsOpen { get; private set; }
        public bool IsAnimating => (animator.IsAnimating());
        public MenuSelector CurrentSelector => stateSelector.ContainsKey(menuState) ? stateSelector[menuState] : null;


        private void Awake()
        {
            mainWindow = GetComponentInChildren<MainWindow>();
            animator = GetComponent<Animator>();
            stateSelector.Add(MenuState.Main, mainSelector);
            stateSelector.Add(MenuState.EquipMemberSelection, memberSelector);
            stateSelector.Add(MenuState.EquipmentSelection, equipmentSelector);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && CurrentSelector.SelectedIndex > 0)
            {
                menuChangeSound.Play();
                CurrentSelector.SelectedIndex--;
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow) && (CurrentSelector.SelectedIndex < CurrentSelector.SelectableOptions.Count - 1))
            {
                menuChangeSound.Play();
                CurrentSelector.SelectedIndex++;
            }

            else if (Input.GetKeyDown(KeyCode.Return))
                Accept();

            else if (Input.GetKeyDown(KeyCode.Escape))
                Cancel();
        }

        public void Open()
        {
            SetMenuState(MenuState.Main);
            mainSelector.SelectedIndex = 0;
            IsOpen = true;
            animator.Play(menuOpenAnimation);
        }

        public void Close()
        {
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
                case (MenuState.EquipMemberSelection):
                    PartyMember selectedMember = Party.ActiveMembers[CurrentSelector.SelectedIndex];
                    mainWindow.ShowEquipmentView(selectedMember);
                    SetMenuState(MenuState.EquipmentSelection);
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
                    memberSelector.SelectedIndex = 0;
                    memberSelector.gameObject.SetActive(false);
                    SetMenuState(MenuState.Main);
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
            memberSelector.gameObject.SetActive(true);
            memberSelector.SelectedIndex = 0;
        }

        private void SetMenuState(MenuState newState)
        {
            CurrentSelector.SelectedIndex = 0;
            CurrentSelector.gameObject.SetActive(false);

            menuState = newState;

            if(stateSelector.ContainsKey(newState))
            {
                CurrentSelector.SelectedIndex = 0;
                CurrentSelector.gameObject.SetActive(true);
            }
        }
    }
}
