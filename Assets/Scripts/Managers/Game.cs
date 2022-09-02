using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    World,
    Cutscene,
    Battle,
    Menu,
}

public class Game : MonoBehaviour
{
    private static DialogueWindow dialogueWindow;
    private static MainMenu mainMenu;

    public static GameState State { get; private set; }
    public static Map Map { get; private set; }
    public static Player Player { get; private set; }
    public static void ToggleMenu()
    {
        if (mainMenu.IsAnimating)
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
    public static void StartDialogue(DialogueScene sceneToPlay)
    {
        State = GameState.Cutscene;
        dialogueWindow.Open(sceneToPlay);
    }
    public static void EndDialogue()
    {
        State = GameState.World;
    }

    [SerializeField] private Map startingMap;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Vector2Int startingCell;
    

    private void Awake()
    {
        mainMenu = FindObjectOfType<MainMenu>();
        dialogueWindow = FindObjectOfType<DialogueWindow>();
        if (Map == null)
        {
            Map = Instantiate(startingMap);
        }
        if (Player == null)
        {
            GameObject gameObject = Instantiate(playerPrefab, startingCell.Center2D(), Quaternion.identity);
            Player = gameObject.GetComponent<Player>();
        }
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(Player);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Battle.EnemyPack = ResourceLoader.Load<EnemyPack>(ResourceLoader.TwoGoblin);
            StartCoroutine(Co_StartBattle());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            EndBattle();
        }
    }

    private IEnumerator Co_StartBattle()
    {
        State = GameState.Battle;
        Instantiate(ResourceLoader.Load<GameObject>(ResourceLoader.BattleTransition), Player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        SceneLoader.LoadBattleScene();
    }

    private void EndBattle()
    {
        SceneLoader.ReloadSavedSceneAfterBattle();
        State = GameState.World;
    } 
}
