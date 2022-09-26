using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Linq;
using Battle;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class B2_TurnBarAndIndicatorTests
{
    private bool isReady = false;
    private BattleControl battleControl;
    private TurnBar sutBar;
    private TurnIndicator sutIndicator;

    [OneTimeSetUp]
    public void SetupScene()
    {
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            Debug.LogError("Test must be run in a working battle scene!");
        }
        else
        {
            battleControl = GameObject.FindObjectOfType<BattleControl>();
            sutBar = GameObject.FindObjectOfType<TurnBar>();
            sutIndicator = GameObject.FindObjectOfType<TurnIndicator>();
            isReady = true;
        }
    }

    [UnityTest, Order(0)]
    public IEnumerator Spawns_correct_number_of_slots()
    {
        // Arrage
        while (!isReady) yield return null;

        // Act

        // Assert
        Assert.AreEqual(battleControl.TurnOrder.Count, sutBar.Slots.Count);
    }

    [Test, Order(1)]
    public void Spawns_correct_number_of_portraits()
    {
        // Arrange
        BattlePortrait[] portraits = GameObject.FindObjectsOfType<BattlePortrait>();

        // Act

        // Assert
        Assert.AreEqual(battleControl.TurnOrder.Count, portraits.Length);
    }

    [UnityTest, Order(2)]
    public IEnumerator Indicator_moves_to_correct_position()
    {
        // Arrange
        RectTransform activeSlotRect = sutBar.Slots[battleControl.TurnNumber].GetComponent<RectTransform>();
        RectTransform indicatorRect = sutIndicator.GetComponent<RectTransform>();
        float elapsedTime = 0;

        // Act


        // Assert
        while (indicatorRect.anchoredPosition != activeSlotRect.anchoredPosition)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            if (elapsedTime > 5f)
                Assert.Fail();
        }

        Assert.Pass();
    }
    



}
