using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Core;

public class A2_TransferTests
{
    private bool isReady = false;
    private Transfer sut;

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
        sut = GameObject.FindObjectOfType<Transfer>();
        isReady = true;
    }

    [UnityTest, Order(0)]
    public IEnumerator Registers_with_map()
    {
        // Arrange
        while (!isReady) yield return null;

        // Act

        // Assert
        Assert.IsTrue(Game.World.Map.TriggerCells.ContainsKey(sut.Cell));
    }

    [UnityTest, Order(1)]
    public IEnumerator Triggers_on_touch()
    {
        // Arrange
        Player player = Game.Player;
        
        // Act -- player should be just below the transfer -- move into it
        player.Movement.TryMove(Direction.Up);
        yield return null;
        while (player.IsMoving) yield return null;
        yield return null;

        // Assert
        Assert.AreEqual(GameState.MapChange, Game.State);
    }

    [UnityTest, Order(2)]
    public IEnumerator Loads_new_map()
    {
        // Arrange
        Map oldMap = Game.World.Map;
        float elapsedTime = 0;
        while (Game.State == GameState.MapChange)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            if (elapsedTime > 3f)
                Assert.Fail();
        }
            
        // Act

        // Assert
        Assert.AreNotEqual(oldMap, Game.World.Map);
        Assert.IsNotNull(Game.World.Map);
    }

    [Test, Order(3)]
    public void Sends_to_correct_position()
    {
        // Arrange -- the sut is now the receiving transfer instead of the sending one
        sut = GameObject.FindObjectOfType<Transfer>();

        // Act

        // Assert
        Assert.AreEqual(sut.Cell + sut.Offset, Game.Player.CurrentCell);
    }

    [Test, Order(4)]
    public void Returns_game_state()
    {
        // Arrange

        // Act

        // Assert
        Assert.AreEqual(GameState.World, Game.State);
    }

}
