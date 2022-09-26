using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Linq;
using Core;
using Battle;

public class B1_BattleControlTests
{
    private bool isReady = false;
    private BattleControl sut;

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
        }
    }

    public void OnSceneReady(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneReady;
        SceneManager.sceneLoaded += OnBattleSceneReady;

        // the test pack is set to 100% encounter, so this should start a fight
        Game.Manager.Player.Movement.TryMove(Direction.Up); 
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
    public void Found_an_enemy_pack()
    {
        // Arrange

        // Act

        // Assert
        Assert.IsNotNull(BattleControl.EnemyPack);
    }

    [UnityTest, Order(2)]
    public IEnumerator Battle_setup_finishes()
    {
        // Arrange
        float elapsedTime = 0;

        // Act
        while (!sut.SetupComplete)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            if (elapsedTime > 3f)
                Assert.Fail();
        }

        Assert.Pass();
    }

    [Test, Order(3)]
    public void Allies_spawned_and_have_stats()
    {
        // Arrange

        // Act

        // Assert
        Assert.AreEqual(Party.ActiveMembers.Count, sut.Allies.Count);

        for (int i = 0; i < Party.ActiveMembers.Count; i++)
        {
            Assert.AreSame(Party.ActiveMembers[i].Stats, sut.Allies[i].Stats);
            Assert.IsTrue(sut.TurnOrder.Contains(sut.Allies[i]));
            Assert.IsTrue(sut.Allies.Contains(sut.Allies[i]));
        }
    }

    [Test, Order(4)]
    public void Enemies_spawned_and_have_stats()
    {
        // Arrange

        // Act

        // Assert
        Assert.AreEqual(BattleControl.EnemyPack.Enemies.Count, sut.Enemies.Count);

        for (int i = 0; i < BattleControl.EnemyPack.Enemies.Count; i++)
        {
            Assert.IsNotNull(sut.Enemies[i].Stats);
            Assert.IsTrue(sut.TurnOrder.Contains(sut.Enemies[i]));
            Assert.IsTrue(sut.Enemies.Contains(sut.Enemies[i]));
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
        float tolerance = .1f;

        // Act

        // Assert
        for (int i = 0; i < BattleControl.EnemyPack.Enemies.Count; i++)
        {
            if (sut.Enemies[i].IsTakingTurn) tolerance = .5f; // enemy that is moving has more tolerance in its location
            Assert.AreEqual(BattleControl.EnemyPack.XSpawnCoordinates[i], sut.Enemies[i].transform.position.x, tolerance);
            Assert.AreEqual(BattleControl.EnemyPack.YSpawnCoordinates[i], sut.Enemies[i].transform.position.y, tolerance);
        }
    }

    [Test, Order(5)]
    public void Turn_number_starts_at_0()
    {
        // Arrange

        // Act

        // Assert
        Assert.AreEqual(0, sut.TurnNumber);
    }

    [UnityTest, Order(6)]
    public IEnumerator First_actor_takes_turn()
    {
        // Arrange
        yield return new WaitForSeconds(.1f);

        // Act
        
        // Assert
        Assert.IsTrue(sut.TurnOrder[0].IsTakingTurn);
    }
}
