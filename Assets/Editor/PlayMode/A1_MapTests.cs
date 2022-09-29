using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Core;

public class A1_MapTests
{
    private bool isReady = false;
    private Map sut;


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
        sut = GameObject.FindObjectOfType<Map>();
        isReady = true;
    }

    [UnityTest, Order(0)]
    public IEnumerator Grid_is_not_null()
    {
        // Arrange
        while (!isReady) yield return null;

        // Act

        // Assert
        Assert.IsNotNull(sut.Grid);
    }

    [Test, Order(1)]
    public void Occupied_cells_are_in_dictionary()
    {
        // Arrange
        Player player = Game.Player;
        NPC npc = GameObject.FindObjectOfType<NPC>();

        // Act

        // Assert
        Assert.AreEqual(2, sut.OccupiedCells.Count);
        sut.OccupiedCells.ContainsKey(player.CurrentCell);
        sut.OccupiedCells.ContainsKey(npc.CurrentCell);
        Assert.AreEqual(player, sut.OccupiedCells[player.CurrentCell]);
        Assert.AreEqual(npc, sut.OccupiedCells[npc.CurrentCell]);
    }

    [Test, Order(2)]
    public void Trigger_cells_are_in_dictionary()
    {
        // Arrange
        Transfer transfer = GameObject.FindObjectOfType<Transfer>();
        Cutscene touchScene = GameObject.Find("TouchScene").GetComponent<Cutscene>();

        // Act

        // Assert
        Assert.AreEqual(2, sut.TriggerCells.Count);
        sut.TriggerCells.ContainsKey(transfer.Cell);
        sut.TriggerCells.ContainsKey(touchScene.Cell);
        Assert.AreEqual(transfer, sut.TriggerCells[transfer.Cell]);
        Assert.AreEqual(touchScene, sut.TriggerCells[touchScene.Cell]);
    }

    [Test, Order(3)]
    public void Map_region_is_null()
    {
        // Arrange

        // Act

        // Assert
        Assert.IsNull(sut.Region);
    }
}
