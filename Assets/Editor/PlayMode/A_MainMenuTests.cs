using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Core;

public class A_MainMenuTests
{
    private bool isReady = false;
    private MainMenu sutMainMenu;
    private MainWindow sutMainWindow;
    private PartyMemberInfo sutMemberInfo;

    [OneTimeSetUp]
    public void SetupScene()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            SceneManager.LoadScene(0);
            SceneManager.sceneLoaded += OnSceneReady;
        }
        else
        {
            OnSceneReady(SceneManager.GetActiveScene(), LoadSceneMode.Single);
            isReady = true;
        }
    }

    public void OnSceneReady(Scene scene, LoadSceneMode mode)
    {
        sutMainMenu = GameObject.FindObjectOfType<MainMenu>();
        sutMainWindow = GameObject.FindObjectOfType<MainWindow>();
        isReady = true;
    }

    [UnityTest, Order(0)]
    public IEnumerator Starts_in_correct_position()
    {
        // Arrange
        while (!isReady || sutMainMenu.IsAnimating) yield return null;

        // Act

        // Assert
        Assert.AreEqual(800, sutMainMenu.GetComponent<RectTransform>().anchoredPosition.x, .1f);
        Assert.AreEqual(0, sutMainMenu.GetComponent<RectTransform>().anchoredPosition.y, .1f);
    }

    [UnityTest, Order(1)]
    public IEnumerator Opens()
    {
        // Arrange

        // Act
        Game.Manager.ToggleMenu();
        yield return null;

        // Assert
        Assert.IsTrue(sutMainMenu.IsOpen);
    }

    [Test, Order(2)]
    public void Changes_game_state()
    {
        // Arrange

        // Act

        // Assert
        Assert.AreEqual(GameState.Menu, Game.Manager.State);
    }

    [UnityTest, Order(3)]
    public IEnumerator Cant_be_closed_while_opening()
    {
        // Arrange
        yield return null;
        
        // Act
        Game.Manager.ToggleMenu();
        yield return null;

        // Assert
        Assert.IsTrue(sutMainMenu.IsAnimating);
        Assert.IsTrue(sutMainMenu.IsOpen);
    }

    [UnityTest, Order(4)]
    public IEnumerator Opens_to_correct_position()
    {
        // Arrange
        while (sutMainMenu.IsAnimating) yield return null;

        // Arrange

        // Assert
        Assert.AreEqual(0, sutMainMenu.GetComponent<RectTransform>().anchoredPosition.x, .1f);
        Assert.AreEqual(0, sutMainMenu.GetComponent<RectTransform>().anchoredPosition.y, .1f);
    }

    [UnityTest, Order(5)]
    public IEnumerator Closes()
    {
        // Arrange

        // Act
        Game.Manager.ToggleMenu();
        yield return null;

        // Assert
        Assert.IsFalse(sutMainMenu.IsOpen);
    }

    [Test, Order(6)]
    public void Releases_game_state()
    {
        // Arrange

        // Act

        // Assert
        Assert.AreEqual(GameState.World, Game.Manager.State);
    }

    [UnityTest, Order(7)]
    public IEnumerator Cant_be_opened_while_closing()
    {
        // Arrange
        yield return null;

        // Act
        Game.Manager.ToggleMenu();
        yield return null;

        // Assert
        Assert.IsTrue(sutMainMenu.IsAnimating);
        Assert.IsFalse(sutMainMenu.IsOpen);
    }

    [UnityTest, Order(8)]
    public IEnumerator Closes_to_correct_position()
    {
        // Arrange
        while (sutMainMenu.IsAnimating) yield return null;

        // Act

        // Assert
        Assert.AreEqual(800, sutMainMenu.GetComponent<RectTransform>().anchoredPosition.x, .1f);
        Assert.AreEqual(0, sutMainMenu.GetComponent<RectTransform>().anchoredPosition.y, .1f);
    }

    [Test, Order(9)]
    public void Generated_party_info()
    {
        // Arrange

        // Act

        // Assert
        Assert.IsNotNull(sutMainWindow.GetComponentsInChildren<PartyMemberInfo>());
    }

    [Test, Order(10)]
    public void Generated_correct_amount_of_party_info()
    {
        // Arrange
        PartyMemberInfo[] partyMemberInfos = sutMainWindow.GetComponentsInChildren<PartyMemberInfo>();

        // Act

        // Assert
        Assert.AreEqual(Party.ActiveMembers.Count, partyMemberInfos.Length);
    }

    [UnityTest, Order(11)]
    public IEnumerator Cant_open_in_cutscene()
    {
        // Arrange
        Dialogue scene = Resources.Load<Dialogue>("ScriptableObjects/DialogueScenes/TestDialogue");
        Game.Manager.StartDialogue(scene);

        // Act
        Game.Manager.ToggleMenu();
        yield return null;

        // Assert
        Assert.IsFalse(sutMainMenu.IsOpen);
        Assert.IsFalse(sutMainMenu.IsAnimating);

        DialogueWindow window = GameObject.FindObjectOfType<DialogueWindow>();
        window.GoToNextLine();
        yield return null;
        window.GoToNextLine();
        yield return null;
        window.GoToNextLine();
        yield return null;
    }
}
