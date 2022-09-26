using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Core;


public class A2_CutsceneTests
{
    private bool isReady = false;
    private Player player;
    private Cutscene sutAuto;
    private Cutscene sutTouch;
    private Cutscene sutCall;

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
        player = Game.Manager.Player;

        // instantiate the autoplaying one so it doesn't mess up other tests
        sutAuto = GameObject.Instantiate(Resources.Load<Cutscene>("TestPrefabs/TestAutoScene")); 

        sutTouch = GameObject.Find("TouchScene").GetComponent<Cutscene>();
        sutCall = GameObject.Find("CallScene").GetComponent<Cutscene>();
        isReady = true;
    }

    [UnityTest, Order(0)]
    public IEnumerator Autoplay_scene_plays()
    {
        while (!isReady) yield return null;

        // Arrange
        yield return null; // the scene starts on Update, so give it a frame to start

        // Act

        // Assert
        Assert.IsNotNull(sutAuto);
        Assert.AreEqual(GameState.Cutscene, Game.Manager.State);
        Assert.IsFalse(sutAuto.IsFinished);
    }

    [UnityTest, Order(1)]
    public IEnumerator Autoplay_scene_ends()
    {
        // Arrange
        float elapsedTime = 0;
        while (!sutAuto.IsFinished)
        {
            yield return null;
            elapsedTime += Time.deltaTime;

            if (elapsedTime > 1f)
                Assert.Fail();
        }

        // Act

        // Assert
        Assert.AreEqual(GameState.World, Game.Manager.State);
        Assert.IsTrue(sutAuto.IsFinished);
    }

    [UnityTest, Order(2)]
    public IEnumerator Touch_scene_plays()
    {
        // Arrange

        // Act -- move the player onto the square with the touch scene
        player.Movement.TryMove(Direction.Up);
        yield return new WaitForSeconds(.5f);

        // Assert -- the touched scene should trigger and move the player left
        Assert.IsFalse(sutTouch.IsFinished);
        Assert.AreEqual(GameState.Cutscene, Game.Manager.State);
        Assert.IsTrue(player.IsMoving);
    }

    [UnityTest, Order(3)]
    public IEnumerator Touch_scene_ends()
    {
        // Arrange
        while (player.IsMoving) yield return null;

        // Act

        // Assert
        Assert.IsTrue(sutTouch.IsFinished);
        Assert.AreEqual(GameState.World, Game.Manager.State);
        Assert.IsFalse(player.IsMoving);
    }

    [UnityTest, Order(4)]
    public IEnumerator Called_scene_plays()
    {
        // Arrange

        // Act
        sutCall.Play();
        yield return null;

        // Assert
        Assert.AreEqual(GameState.Cutscene, Game.Manager.State);
        Assert.IsFalse(sutCall.IsFinished);
        Assert.IsTrue(player.IsMoving);
    }

    [UnityTest, Order(5)]
    public IEnumerator Called_scene_ends()
    {
        // Arrange
        float elapsedTime = 0;
        while (!sutCall.IsFinished)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            if (elapsedTime > 1f)
                Assert.Fail();
        }

        // Act

        // Assert
        Assert.IsTrue(sutCall.IsFinished);
        Assert.AreEqual(GameState.World, Game.Manager.State);
    }
}