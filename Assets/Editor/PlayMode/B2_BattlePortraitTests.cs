using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Linq;
using Battle;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class B2_BattlePortraitTests
{
    private bool isReady = false;
    private BattleControl battleControl;
    private TurnBar turnBar;
    private List<BattlePortrait> suts;

    [OneTimeSetUp]
    public void SetupScene()
    {
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            Debug.LogError("Test must be run in a working battle scene!");
        }
        else
        {
            suts = GameObject.FindObjectsOfType<BattlePortrait>().ToList();
            battleControl = GameObject.FindObjectOfType<BattleControl>();
            turnBar = GameObject.FindObjectOfType<TurnBar>();
            isReady = true;
        }
    }

    [UnityTest, Order(0)]
    public IEnumerator All_portraits_spawned()
    {
        // Arrange
        while (!isReady) yield return null;

        // Act

        // Assert
        Assert.AreEqual(battleControl.TurnOrder.Count, suts.Count());
    }

    [Test, Order(1)]
    public void Turnbar_is_parent()
    {
        // Arrange

        // Act

        // Assert
        foreach(BattlePortrait portrait in suts)
        {
            Assert.AreEqual(turnBar.transform, portrait.transform.parent);
        }
    }

    [UnityTest, Order(2)]
    public IEnumerator Moves_to_slot_position()
    {
        // Arrange
        List<GameObject> slots = turnBar.Slots;
        float elapsedTime = 0;

        // Act

        // Assert
        while (elapsedTime < 5f)
        {
            yield return null;
            int successes = 0;
            for (int i = 0; i < suts.Count; i++)
            {
                for (int j = 0; j < slots.Count; j++)
                {
                    if (suts[i].GetComponent<RectTransform>().anchoredPosition == slots[j].GetComponent<RectTransform>().anchoredPosition)
                        successes++;
                }
            }
            if (successes == suts.Count)
                Assert.Pass();
            elapsedTime += Time.deltaTime;
        }

        Assert.Fail();
    }

    [Test, Order(3)]
    public void One_portrait_per_slot()
    {
        // Arrange

        // Assert
        for(int i = 0; i < suts.Count; i++)
        {
            for (int j = 0; j < suts.Count; j++)
            {
                if (i == j) continue;
                if (suts[i].GetComponent<RectTransform>().anchoredPosition == suts[j].GetComponent<RectTransform>().anchoredPosition)
                    Assert.Fail();
            }
        }

        Assert.Pass();
    }



}
