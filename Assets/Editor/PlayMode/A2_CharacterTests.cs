using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Core;


public class A2_CharacterTests
{
    private bool isReady = false;
    private Player sut;

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
        sut = Game.Player;
        isReady = true;
    }

    [UnityTest, Order(0)]
    public IEnumerator Finds_current_cell()
    {
        while (!isReady) yield return null;

        // Arrange
        Vector2Int startingCell = new Vector2Int(0, 3);

        // Act

        // Assert
        Assert.AreEqual(startingCell, sut.CurrentCell);
    }

    [Test, Order(1)]
    public void Character_facing_updates_correctly()
    {
        // Arrange

        // Act & Assert
        sut.Turner.Turn(Direction.Left);
        Assert.AreEqual(Direction.Left, sut.Facing);

        sut.Turner.Turn(Direction.Down);
        Assert.AreEqual(Direction.Down, sut.Facing);

        sut.Turner.Turn(Direction.Right);
        Assert.AreEqual(Direction.Right, sut.Facing);

        sut.Turner.Turn(Direction.Up);
        Assert.AreEqual(Direction.Up, sut.Facing);
    }

    [UnityTest, Order(2)]
    public IEnumerator Moves_to_the_correct_cell()
    {
        // Arrange

        // Act & Assert

        // Left
        Vector2Int current = sut.CurrentCell;
        sut.Movement.TryMove(Direction.Left);
        while (sut.IsMoving) yield return null;
        Assert.AreEqual(current + Direction.Left, sut.CurrentCell);

        // Right
        current = sut.CurrentCell;
        sut.Movement.TryMove(Direction.Right);
        while (sut.IsMoving) yield return null;
        Assert.AreEqual(current + Direction.Right, sut.CurrentCell);

        // Down
        current = sut.CurrentCell;
        sut.Movement.TryMove(Direction.Down);
        while (sut.IsMoving) yield return null;
        Assert.AreEqual(current + Direction.Down, sut.CurrentCell);

        // Up
        current = sut.CurrentCell;
        sut.Movement.TryMove(Direction.Up);
        while (sut.IsMoving) yield return null;
        Assert.AreEqual(current + Direction.Up, sut.CurrentCell);
    }

    [UnityTest, Order(3)]
    public IEnumerator Updates_cell_map_dictionary()
    {
        // Arrange
        Vector2Int originalCell = sut.CurrentCell;
        Assert.IsTrue(Game.World.Map.OccupiedCells.ContainsKey(originalCell));
        Assert.AreEqual(sut, Game.World.Map.OccupiedCells[originalCell]);

        // Act
        sut.Movement.TryMove(Direction.Left);
        while (sut.IsMoving) yield return null;

        // Assert
        Assert.IsTrue(Game.World.Map.OccupiedCells.ContainsKey(sut.CurrentCell));
        Assert.IsFalse(Game.World.Map.OccupiedCells.ContainsKey(originalCell));
        Assert.AreEqual(sut, Game.World.Map.OccupiedCells[sut.CurrentCell]);
    }

    [UnityTest, Order(4)]
    public IEnumerator Cant_move_into_characters()
    {
        // Arrange
        sut.Movement.TryMove(Direction.Down); // move next to the NPC
        yield return null;
        while (sut.IsMoving) yield return null;
        Vector2Int originalCell = sut.CurrentCell;

        // Act
        sut.Movement.TryMove(Direction.Left); // try to move into the NPC
        yield return null;

        // Assert
        Assert.IsFalse(sut.IsMoving);
    }

    [Test, Order(5)]
    public void Failed_move_changes_facing()
    {
        // Arrange

        // Act
        sut.Movement.TryMove(Direction.Down);

        // Assert
        Assert.AreEqual(Direction.Down, sut.Facing);
    }

    [UnityTest, Order(6)]
    public IEnumerator Cant_move_into_collisions()
    {
        // Arrange
        Vector2Int originalCell = sut.CurrentCell;

        // Act
        sut.Movement.TryMove(Direction.Down); // try to move into the rock on the tilemap
        yield return null;

        // Assert
        Assert.IsFalse(sut.IsMoving);
    }

    [UnityTest, Order(7)]
    public IEnumerator Animator_updates_parameters()
    {
        // Arrange
        Animator animator = sut.GetComponent<Animator>();
        CharacterAnimator charAnimator = sut.Animator;
        Assert.IsFalse(animator.GetBool(charAnimator.WalkingParameter));
        Assert.AreEqual(0, animator.GetFloat(charAnimator.HorizontalParameter));
        Assert.AreEqual(-1.0f, animator.GetFloat(charAnimator.VerticalParameter));

        // Act & Assert
        sut.Movement.TryMove(Direction.Up);
        yield return null;
        Assert.IsTrue(animator.GetBool(charAnimator.WalkingParameter));
        Assert.AreEqual(0, animator.GetFloat(charAnimator.HorizontalParameter));
        Assert.AreEqual(1.0f, animator.GetFloat(charAnimator.VerticalParameter));
        while (sut.IsMoving) yield return null;

        sut.Movement.TryMove(Direction.Right);
        yield return null;
        Assert.IsTrue(animator.GetBool(charAnimator.WalkingParameter));
        Assert.AreEqual(1.0f, animator.GetFloat(charAnimator.HorizontalParameter));
        Assert.AreEqual(0.0f, animator.GetFloat(charAnimator.VerticalParameter));
    }
}
