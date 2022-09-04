using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class A_DialogueWindowTests
{
    private bool isReady = false;
    private DialogueWindow sut;
    private DialogueScene testScene;
    private Animator sutAnimator;

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
        sut = GameObject.FindObjectOfType<DialogueWindow>();
        testScene = Resources.Load<DialogueScene>("ScriptableObjects/DialogueScenes/TestDialogue");
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
        Game.StartDialogue(testScene);
        yield return null;

        // Assert
        Assert.IsTrue(sutAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
    }

    [UnityTest, Order(2)]
    public IEnumerator Opens_to_correct_position()
    {
        // Arrange
        while (sutAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) 
            yield return null;

        // Act

        // Assert
        Assert.AreEqual(-200.0f, sut.GetComponent<RectTransform>().anchoredPosition.y, .1f);
    }

    [UnityTest, Order(3)]
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
        Assert.AreEqual(GameState.World, Game.State);
    }

    [Test, Order(4)]
    public void Returns_game_state_to_normal_when_done()
    {
        // Arrange

        // Act

        // Assert
        Assert.AreEqual(GameState.World, Game.State);
    }

    [UnityTest, Order(5)]
    public IEnumerator Closes_to_correct_position()
    {
        // Arrange
        while (sutAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            yield return null;

        // Act

        // Assert
        Assert.AreEqual(-380.0f, sut.GetComponent<RectTransform>().anchoredPosition.y, .1f);
    }
}
