using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BattleStatsTests
{
    private BattleStats sut1;
    private BattleStats sut2;

    [SetUp]
    public void SetupStats()
    {
        sut1 = new BattleStats(1, 10, 10, 4, 2, 2);
        /* Level 1
         * 10 HP, 10 MaxHP
         * 4 STR
         * 2 ARM
         * 2 SPD*/

        sut2 = new BattleStats(3, 15, 15, 6, 4, 5);
        /* Level 3
         * 15 HP, 15 MaxHP
         * 6 STR
         * 4 ARM
         * 5 SPD*/
    }

    [Test]
    public void Level_clamped_correctly()
    {
        // Arrange

        // Act
        sut1.Level = 903;
        sut2.Level = 0;

        // Assert
        Assert.AreEqual(99, sut1.Level);
        Assert.AreEqual(1, sut2.Level);
    }

    [Test]
    public void MaxHP_clamped_correctly()
    {
        // Arrange

        // Act
        sut1.MaxHP = 3432;
        sut2.MaxHP = -5;

        // Assert
        Assert.AreEqual(999, sut1.MaxHP);
        Assert.AreEqual(1, sut2.MaxHP);
    }

    [Test]
    public void Hp_clamped_correctly()
    {
        // Arrange

        // Act
        sut1.HP = 312;
        sut2.HP = -999;

        // Assert
        Assert.AreEqual(sut1.MaxHP, sut1.HP);
        Assert.AreEqual(0, sut2.HP);
    }

    [Test]
    public void Str_clamped_correctly()
    {
        // Arrange

        // Act
        sut1.STR = 100;
        sut2.STR = -100;

        // Assert
        Assert.AreEqual(99, sut1.STR);
        Assert.AreEqual(0, sut2.STR);
    }

    [Test]
    public void Arm_clamped_correctly()
    {
        // Arrange

        // Act
        sut1.ARM = 100;
        sut2.ARM = -100;

        // Assert
        Assert.AreEqual(99, sut1.ARM);
        Assert.AreEqual(0, sut2.ARM);
    }

    [Test]
    public void Spd_clamped_correctly()
    {
        // Arrange

        // Act
        sut1.SPD = 100;
        sut2.SPD = -100;

        // Assert
        Assert.AreEqual(99, sut1.SPD);
        Assert.AreEqual(0, sut2.SPD);
    }

    [Test]
    public void Initiative_returns_correct_range()
    {
        // Arrange

        // Act
        int initiative1 = sut1.Initiative;
        int initiative2 = sut2.Initiative;

        // Assert
        Assert.LessOrEqual(initiative1, 3);
        Assert.GreaterOrEqual(initiative1, 1);
        Assert.LessOrEqual(initiative2, 6);
        Assert.GreaterOrEqual(initiative2, 4);
    }
}
