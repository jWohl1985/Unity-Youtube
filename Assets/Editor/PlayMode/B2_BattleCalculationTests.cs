using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Linq;
using Core;
using Battle;

public class B2_BattleCalculationTests
{
    private bool isReady = false;
    private Ally ally;
    private Enemy enemy;

    [OneTimeSetUp]
    public void SetupScene()
    {
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            Debug.LogError("Test must be run in a working battle scene!");
        }
        else
        {
            BattleControl battleControl = GameObject.FindObjectOfType<BattleControl>();
            ally = battleControl.Allies[0];
            enemy = battleControl.Enemies[0];
            isReady = true;
        }
    }

    [Test]
    public void Attack_damage_calculates_correctly()
    {
        int damage;

        // Arrange, Act, Assert
        ally.Stats.STR = 15;
        enemy.Stats.ARM = 10;
        damage = BattleCalculations.AttackDamage(ally, enemy);
        Assert.AreEqual(5, damage);

        // Arrange, Act, Assert
        ally.Stats.STR = 10;
        enemy.Stats.ARM = 10;
        damage = BattleCalculations.AttackDamage(ally, enemy);
        Assert.AreEqual(0, damage);

        // Arrange, Act, Assert
        ally.Stats.STR = 9;
        enemy.Stats.ARM = 10;
        damage = BattleCalculations.AttackDamage(ally, enemy);
        Assert.AreEqual(0, damage);

        // Arrange, Act, Assert
        ally.Stats.STR = 9999999; // this should get capped to 99
        enemy.Stats.ARM = -5; // this should get floored at 0
        damage = BattleCalculations.AttackDamage(ally, enemy);
        Assert.AreEqual(99, damage);
    }
}
