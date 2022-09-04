using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Core;


public class A_CharacterTests
{
    private bool isReady = false;
    private Player sut;

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
            isReady = true;
        }
    }

    public void OnSceneReady(Scene scene, LoadSceneMode mode)
    {
        sut = Game.Manager.Player;
        isReady = true;
    }

    [UnityTest, Order(0)]
    public IEnumerator Finds_current_cell()
    {
        while (!isReady) yield return null;
        Assert.AreEqual(new Vector2Int(0, 0), sut.CurrentCell);
    }

    [Test, Order(1)]
    public void Character_facing_updates_correctly()
    {
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
        // Moving Left
        Vector2Int current = sut.CurrentCell;
        sut.Movement.TryMove(Direction.Left);
        yield return new WaitForSeconds(.5f);
        Assert.AreEqual(current + Direction.Left, sut.CurrentCell);

        // Moving Right
        current = sut.CurrentCell;
        sut.Movement.TryMove(Direction.Right);
        yield return new WaitForSeconds(.5f);
        Assert.AreEqual(current + Direction.Right, sut.CurrentCell);

        // Moving Down
        current = sut.CurrentCell;
        sut.Movement.TryMove(Direction.Down);
        yield return new WaitForSeconds(.5f);
        Assert.AreEqual(current + Direction.Down, sut.CurrentCell);

        // Moving Up
        current = sut.CurrentCell;
        sut.Movement.TryMove(Direction.Up);
        yield return new WaitForSeconds(.5f);
        Assert.AreEqual(current + Direction.Up, sut.CurrentCell);
    }

    [UnityTest, Order(3)]
    public IEnumerator Updates_cell_map_dictionary()
    {
        Vector2Int originalCell = sut.CurrentCell;

        Assert.IsTrue(Game.Manager.Map.OccupiedCells.ContainsKey(originalCell));
        Assert.AreEqual(sut, Game.Manager.Map.OccupiedCells[originalCell]);

        sut.Movement.TryMove(Direction.Left);
        yield return new WaitForSeconds(.5f);

        Assert.IsTrue(Game.Manager.Map.OccupiedCells.ContainsKey(sut.CurrentCell));
        Assert.IsFalse(Game.Manager.Map.OccupiedCells.ContainsKey(originalCell));
        Assert.AreEqual(sut, Game.Manager.Map.OccupiedCells[sut.CurrentCell]);
    }

    [UnityTest, Order(4)]
    public IEnumerator Cant_move_into_characters()
    {
        sut.Movement.TryMove(Direction.Down);
        yield return new WaitForSeconds(.5f);

        Vector2Int originalCell = sut.CurrentCell;
        sut.Movement.TryMove(Direction.Left);
        yield return new WaitForSeconds(.5f);
        Assert.AreEqual(originalCell, sut.CurrentCell);
    }

    [Test, Order(5)]
    public void Failed_move_changes_facing()
    {
        sut.Movement.TryMove(Direction.Down);
        Assert.AreEqual(Direction.Down, sut.Facing);
    }

    [UnityTest, Order(6)]
    public IEnumerator Cant_move_into_collisions()
    {
        Vector2Int originalCell = sut.CurrentCell;
        sut.Movement.TryMove(Direction.Down);
        yield return new WaitForSeconds(.5f);
        Assert.AreEqual(originalCell, sut.CurrentCell);
    }

    [UnityTest, Order(7)]
    public IEnumerator Animator_updates_parameters()
    {
        Animator animator = sut.GetComponent<Animator>();
        CharacterAnimator charAnimator = sut.Animator;

        Assert.IsFalse(animator.GetBool(charAnimator.WalkingParameter));
        Assert.AreEqual(0, animator.GetFloat(charAnimator.HorizontalParameter));
        Assert.AreEqual(-1.0f, animator.GetFloat(charAnimator.VerticalParameter));

        sut.Movement.TryMove(Direction.Up);

        yield return new WaitForSeconds(.1f);

        Assert.IsTrue(animator.GetBool(charAnimator.WalkingParameter));
        Assert.AreEqual(0, animator.GetFloat(charAnimator.HorizontalParameter));
        Assert.AreEqual(1.0f, animator.GetFloat(charAnimator.VerticalParameter));

        yield return new WaitForSeconds(.4f);

        sut.Movement.TryMove(Direction.Right);

        yield return new WaitForSeconds(.1f);

        Assert.IsTrue(animator.GetBool(charAnimator.WalkingParameter));
        Assert.AreEqual(1.0f, animator.GetFloat(charAnimator.HorizontalParameter));
        Assert.AreEqual(0.0f, animator.GetFloat(charAnimator.VerticalParameter));
    }
}
