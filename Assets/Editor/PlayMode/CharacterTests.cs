using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class CharacterTests
{
    private bool isReady = false;
    private Player sut;

    [OneTimeSetUp]
    public void LoadTestScene()
    {
        SceneManager.LoadScene(0);
        SceneManager.sceneLoaded += SceneReady;
    }

    public void SceneReady(Scene scene, LoadSceneMode mode)
    {
        sut = GameObject.FindObjectOfType<Player>();
        isReady = true;
    }

    // Should start in cell 0,0
    [UnityTest, Order(0)]
    public IEnumerator Finds_current_cell()
    {
        while (!isReady) yield return null;
        Assert.AreEqual(new Vector2Int(0, 0), sut.CurrentCell);
    }

    // Turns in place a few times
    [Test, Order(1)]
    public void Character_facing_updates_correctly()
    {
        sut.Turn.Turn(Direction.Left);
        Assert.AreEqual(Direction.Left, sut.Facing);

        sut.Turn.Turn(Direction.Down);
        Assert.AreEqual(Direction.Down, sut.Facing);

        sut.Turn.Turn(Direction.Right);
        Assert.AreEqual(Direction.Right, sut.Facing);

        sut.Turn.Turn(Direction.Up);
        Assert.AreEqual(Direction.Up, sut.Facing);
    }

    // Moves up, down, left, right, ending in the same spot where we started
    [UnityTest, Order(2)]
    public IEnumerator Moves_to_the_correct_cell()
    {
        // Moving Left
        Vector2Int current = sut.CurrentCell;
        sut.Move.TryMove(Direction.Left);
        yield return new WaitForSeconds(.5f);
        Assert.AreEqual(current + Direction.Left, sut.CurrentCell);

        // Moving Right
        current = sut.CurrentCell;
        sut.Move.TryMove(Direction.Right);
        yield return new WaitForSeconds(.5f);
        Assert.AreEqual(current + Direction.Right, sut.CurrentCell);

        // Moving Down
        current = sut.CurrentCell;
        sut.Move.TryMove(Direction.Down);
        yield return new WaitForSeconds(.5f);
        Assert.AreEqual(current + Direction.Down, sut.CurrentCell);

        // Moving Up
        current = sut.CurrentCell;
        sut.Move.TryMove(Direction.Up);
        yield return new WaitForSeconds(.5f);
        Assert.AreEqual(current + Direction.Up, sut.CurrentCell);
    }

    // Moves left once, checking that the occupied map cells update
    [UnityTest, Order(3)]
    public IEnumerator Updates_cell_map_dictionary()
    {
        Vector2Int originalCell = sut.CurrentCell;

        Assert.IsTrue(Game.Map.OccupiedCells.ContainsKey(originalCell));
        Assert.AreEqual(sut, Game.Map.OccupiedCells[originalCell]);

        sut.Move.TryMove(Direction.Left);
        yield return new WaitForSeconds(.5f);

        Assert.IsTrue(Game.Map.OccupiedCells.ContainsKey(sut.CurrentCell));
        Assert.IsFalse(Game.Map.OccupiedCells.ContainsKey(originalCell));
        Assert.AreEqual(sut, Game.Map.OccupiedCells[sut.CurrentCell]);
    }

    // Moves next to the NPC and try to move into him
    [UnityTest, Order(4)]
    public IEnumerator Cant_move_into_characters()
    {
        sut.Move.TryMove(Direction.Down);
        yield return new WaitForSeconds(.5f);

        Vector2Int originalCell = sut.CurrentCell;
        sut.Move.TryMove(Direction.Left);
        yield return new WaitForSeconds(.5f);
        Assert.AreEqual(originalCell, sut.CurrentCell);
    }

    // Tries to move into the fence below -- shouldn't move, but should turn
    [Test, Order(5)]
    public void Failed_move_changes_facing()
    {
        sut.Move.TryMove(Direction.Down);
        Assert.AreEqual(Direction.Down, sut.Facing);
    }

    // Tries to move into the fence below -- shouldn't be able to
    [UnityTest, Order(6)]
    public IEnumerator Cant_move_into_collisions()
    {
        Vector2Int originalCell = sut.CurrentCell;
        sut.Move.TryMove(Direction.Down);
        yield return new WaitForSeconds(.5f);
        Assert.AreEqual(originalCell, sut.CurrentCell);
    }

    // Moves up, checks to make sure animation parameters update correctly
    [UnityTest, Order(7)]
    public IEnumerator Animator_updates_parameters()
    {
        Animator animator = sut.GetComponent<Animator>();
        CharacterAnimator charAnimator = sut.Animator;

        Assert.IsFalse(animator.GetBool(charAnimator.WalkingParameter));
        Assert.AreEqual(0, animator.GetFloat(charAnimator.HorizontalParameter));
        Assert.AreEqual(-1.0f, animator.GetFloat(charAnimator.VerticalParameter));

        sut.Move.TryMove(Direction.Up);

        yield return new WaitForSeconds(.1f);

        Assert.IsTrue(animator.GetBool(charAnimator.WalkingParameter));
        Assert.AreEqual(0, animator.GetFloat(charAnimator.HorizontalParameter));
        Assert.AreEqual(1.0f, animator.GetFloat(charAnimator.VerticalParameter));

        yield return new WaitForSeconds(.4f);

        sut.Move.TryMove(Direction.Right);

        yield return new WaitForSeconds(.1f);

        Assert.IsTrue(animator.GetBool(charAnimator.WalkingParameter));
        Assert.AreEqual(1.0f, animator.GetFloat(charAnimator.HorizontalParameter));
        Assert.AreEqual(0.0f, animator.GetFloat(charAnimator.VerticalParameter));
    }
}
