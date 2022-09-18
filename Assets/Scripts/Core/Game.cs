using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Battle;
using System.Linq;

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
        [SerializeField] private GameObject mapTransitionPrefab;

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
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
                Game.Manager.EndBattle();
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
            Animator animator = PlayTransition(TransitionType.Battle);
            while (animator.IsAnimating()) yield return null;
            SceneLoader.LoadBattleScene();
        }

        public void EndBattle()
        {
            SceneLoader.ReloadSavedSceneAfterBattle();
            State = GameState.World;
        }

        private Animator PlayTransition(TransitionType type)
        {
            GameObject transitionPrefab = mapTransitionPrefab;
            if (type == TransitionType.Battle)
                transitionPrefab = battleTransitionPrefab;

            Animator animator = Instantiate(transitionPrefab, Player.transform.position, Quaternion.identity).GetComponent<Animator>();
            return animator;
        }

        public void AdvanceDialogue()
        {
            if (!dialogueWindow.IsOpen || State != GameState.Cutscene || dialogueWindow.IsAnimating)
                return;

            dialogueWindow.GoToNextLine();
        }

        public void LoadMap(Map newMap, int destinationId) => StartCoroutine(Co_LoadMap(newMap, destinationId));

        private IEnumerator Co_LoadMap(Map newMap, int destinationId)
        {
            State = GameState.Loading;

            Animator animator = PlayTransition(TransitionType.MapChange);
            while (animator.IsAnimating())
                yield return null;

            Map oldMap = Map;
            Map = Instantiate(newMap);
            Destroy(oldMap.gameObject);

            Transfer[] transfers = FindObjectsOfType<Transfer>();
            Transfer transfer = transfers.Where(transfer => transfer.Id == destinationId).ToList().FirstOrDefault();
            Player.transform.position = transfer.Cell.Center2D();

            animator.Play("MapTransition_FadeIn");
            yield return null;
            while (animator.IsAnimating())
                yield return null;

            Destroy(animator.gameObject);

            State = GameState.World;
        }
    }
}
