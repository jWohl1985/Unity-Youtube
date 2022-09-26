using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Core;

public class A1_DialogueWindowTests
{
    private bool isReady = false;
    private DialogueWindow sut;
    private List<DialogueLine> testDialogue;
    private Animator sutAnimator;

    [OneTimeSetUp]
    public void SetupScene()
    {
        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            SceneManager.LoadScene(3);
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
        sut = GameObject.FindObjectOfType<DialogueWindow>();
        testDialogue = new List<DialogueLine>();
        testDialogue.Add(new DialogueLine());
        testDialogue.Add(new DialogueLine());
        testDialogue.Add(new DialogueLine());
        sutAnimator = sut.GetComponent<Animator>();
        isReady = true;
    }

    [UnityTest, Order(0)]
    public IEnumerator Starts_in_correct_position()
    {
        // Arrange
        while (!isReady) yield return null;

        // Act

        // Assert
        Assert.AreEqual(-380.0f, sut.GetComponent<RectTransform>().anchoredPosition.y, .1f);
    }

    [UnityTest, Order(1)]
    public IEnumerator Opens()
    {
        // Arrange

        // Act
        Game.Manager.StartDialogue(testDialogue);
        yield return null;

        // Assert
        Assert.IsTrue(sutAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
    }

    [Test, Order(2)]
    public void Changes_game_state()
    {
        // Arrange

        // Act

        // Assert
        Assert.AreEqual(GameState.Dialogue, Game.Manager.State);
    }

    [UnityTest, Order(3)]
    public IEnumerator Opens_to_correct_position()
    {
        // Arrange
        while (sut.IsAnimating) 
            yield return null;

        // Act

        // Assert
        Assert.AreEqual(-200.0f, sut.GetComponent<RectTransform>().anchoredPosition.y, .1f);
    }

    [UnityTest, Order(4)]
    public IEnumerator Goes_through_dialogue()
    {
        // Arrange

        // Act
        sut.GoToNextLine();
        yield return null;
        sut.GoToNextLine();
        yield return null;
        sut.GoToNextLine();
        yield return null;

        // Assert
        Assert.AreEqual(GameState.World, Game.Manager.State);
    }

    [Test, Order(5)]
    public void Releases_game_state()
    {
        // Arrange

        // Act

        // Assert
        Assert.AreEqual(GameState.World, Game.Manager.State);
    }

    [UnityTest, Order(6)]
    public IEnumerator Closes_to_correct_position()
    {
        // Arrange
        while (sut.IsAnimating)
            yield return null;

        // Act

        // Assert
        Assert.AreEqual(-380.0f, sut.GetComponent<RectTransform>().anchoredPosition.y, .1f);
    }
}
