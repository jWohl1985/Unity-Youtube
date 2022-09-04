using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Linq;
using Core;
using Battle;

public class B_BattleTests
{
    private bool isReady = false;
    private BattleControl sut;

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
        }
    }

    public void OnSceneReady(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneReady;
        SceneManager.sceneLoaded += OnBattleSceneReady;
        Game game = GameObject.FindObjectOfType<Game>();
        BattleControl.EnemyPack = Resources.Load<EnemyPack>(Paths.TwoGoblin);
        game.StartCoroutine(game.Co_StartBattle(Resources.Load<EnemyPack>(Paths.TwoGoblin)));
    }

    public void OnBattleSceneReady(Scene scene, LoadSceneMode mode)
    {
        sut = GameObject.FindObjectOfType<BattleControl>();
        isReady = true;
    }

    [UnityTest, Order(0)]
    public IEnumerator Game_state_changes()
    {
        // Arrange
        while (!isReady) yield return null;

        // Act

        // Assert
        Assert.AreEqual(GameState.Battle, Game.Manager.State);
    }

    [Test, Order(1)]
    public void Facing_correct_pack()
    {
        // Arrange

        // Act

        // Assert
        Assert.AreEqual(BattleControl.EnemyPack, Resources.Load<EnemyPack>(Paths.TwoGoblin));
    }

    [UnityTest, Order(1)]
    public IEnumerator Allies_spawned()
    {
        // Arrange
        while (!sut.SetupComplete) yield return null;

        // Act

        // Assert
        Assert.AreEqual(Party.ActiveMembers.Count, sut.Allies.Count);

        for (int i = 0; i < Party.ActiveMembers.Count; i++)
        {
            Assert.AreSame(Party.ActiveMembers[i].Stats, sut.Allies[i].Stats);
            Assert.IsTrue(sut.TurnOrder.Contains(sut.Allies[i]));
        }
    }

    [Test, Order(2)]
    public void Enemies_spawned()
    {
        // Arrange

        // Act

        // Assert
        Assert.AreEqual(BattleControl.EnemyPack.Enemies.Count, sut.Enemies.Count);

        for (int i = 0; i < BattleControl.EnemyPack.Enemies.Count; i++)
        {
            Assert.AreSame(BattleControl.EnemyPack.Enemies[i].Stats, sut.Enemies[i].Stats);
            Assert.IsTrue(sut.TurnOrder.Contains(sut.Enemies[i]));
        }
    }

    [Test, Order(3)]
    public void Turn_order_based_on_initiative()
    {
        // Arrange

        // Act

        // Assert
        for (int i = 0; i < (sut.TurnOrder.Count - 1); i++)
        {
            Assert.IsTrue((sut.TurnOrder[i].Stats.SPD + 1) >= (sut.TurnOrder[i + 1].Stats.SPD - 1));
        }
    }

    [Test, Order(4)]
    public void Enemies_spawn_in_correct_positions()
    {
        // Arrange

        // Act

        // Assert
        for (int i = 0; i < BattleControl.EnemyPack.Enemies.Count; i++)
        {
            Assert.AreEqual(BattleControl.EnemyPack.XSpawnCoordinates[i], sut.Enemies[i].transform.position.x, .1f);
            Assert.AreEqual(BattleControl.EnemyPack.YSpawnCoordinates[i], sut.Enemies[i].transform.position.y, .1f);
        }
    }

    [UnityTest, Order(5)]
    public IEnumerator First_actor_takes_turn_and_goes_to_middle()
    {
        // Arrange
        yield return new WaitForSeconds(.1f);

        // Act
        
        // Assert
        Assert.IsTrue(sut.TurnOrder[0].IsTakingTurn);
    }
}
