using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Battle;

namespace Core
{
    public class Game : MonoBehaviour
    {
        public static Game Manager { get; private set; }

        [SerializeField] private Map startingMap;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Vector2Int startingCell;
        [SerializeField] private DialogueWindow dialogueWindow;
        [SerializeField] private MainMenu mainMenu;
        [SerializeField] private GameObject battleTransitionPrefab;

        public GameState State { get; private set; }
        public Map Map { get; private set; }
        public Player Player { get; private set; }

        private void Awake()
        {
            if (Manager != null && Manager != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Manager = this;
            }

            Map = Instantiate(startingMap);
            Player = Instantiate(playerPrefab, startingCell.Center2D(), Quaternion.identity).GetComponent<Player>();
            DontDestroyOnLoad(Player);
            DontDestroyOnLoad(Map);
            DontDestroyOnLoad(this);
        }

        public void ToggleMenu()
        {
            if (mainMenu.IsAnimating || State == GameState.Cutscene)
                return;

            if (mainMenu.IsOpen)
            {
                State = GameState.World;
                mainMenu.Close();
            }
            else
            {
                State = GameState.Menu;
                mainMenu.Open();
            }
        }

        public void StartDialogue(Dialogue sceneToPlay)
        {
            State = GameState.Cutscene;
            dialogueWindow.Open(sceneToPlay);
        }

        public void EndDialogue() => State = GameState.World;

        public IEnumerator Co_StartBattle(EnemyPack pack)
        {
            State = GameState.Battle;
            BattleControl.EnemyPack = pack;
            Animator animator = PlayBattleTransition();
            while (animator.IsAnimating()) yield return null;
            SceneLoader.LoadBattleScene();
        }

        public void EndBattle()
        {
            SceneLoader.ReloadSavedSceneAfterBattle();
            State = GameState.World;
        }

        private Animator PlayBattleTransition()
        {
            Animator animator = Instantiate(battleTransitionPrefab, Player.transform.position, Quaternion.identity).GetComponent<Animator>();
            return animator;
        }
    }
}
