using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Core;

public class A2_NPCTests
{
    private bool isReady = false;
    private NPC sut;

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
        sut = GameObject.FindObjectOfType<NPC>();
        sut.Delay = .02f; // set a shorter delay to make the tests faster
        isReady = true;
    }

    [UnityTest, Order(0)]
    public IEnumerator Doesnt_move_if_never_moves()
    {
        // Arrange
        while (!isReady || Game.State != GameState.World) yield return null;
        float timeElapsed = 0;

        // Act
        sut.NeverMoves = true;
       
        // Assert
        while(timeElapsed < (sut.Delay + .5f))
        {
            if (sut.IsMoving == true)
            {
                Assert.Fail();
            }
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    [UnityTest, Order(1)]
    public IEnumerator Moves_if_randomly_moves()
    {
        // Arrange
        float timeElapsed = 0;
        sut.NeverMoves = false;

        // Act
        sut.MovesRandomly = true;
        yield return new WaitForSeconds(sut.Delay); // the first movement won't start until the delay is over

        // Assert
        while (timeElapsed < .5f)
        {
            if (sut.IsMoving == true)
            {
                Assert.Pass();
                yield break;
            }
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        Assert.Fail();
    }

    [UnityTest, Order(2)]
    public IEnumerator Does_move_route_once_if_not_stationary_or_random()
    {
        // Arrange
        sut.NeverMoves = true; // hold still until we're ready
        while (sut.IsMoving) yield return null; // finish any current movement
        sut.MoveRouteLoops = false;
        sut.MovesRandomly = false;
        Vector2Int oldPosition = sut.CurrentCell;

        // Act
        sut.NeverMoves = false;
        yield return new WaitForSeconds(sut.Delay); // the first movement won't start until the delay is over
        yield return null;

        // Assert
            // Trigger a movement, wait for it to finish, assert the new position, then wait for the delay + 1 frame
            // The move route should be set to Up, Down, Left, Right
        while (sut.IsMoving) yield return null;
        Assert.AreEqual(oldPosition + Direction.Up, sut.CurrentCell);
        oldPosition = sut.CurrentCell;
        yield return new WaitForSeconds(sut.Delay);
        yield return null;

        while (sut.IsMoving) yield return null;
        Assert.AreEqual(oldPosition + Direction.Down, sut.CurrentCell);
        oldPosition = sut.CurrentCell;
        yield return new WaitForSeconds(sut.Delay);
        yield return null;

        while (sut.IsMoving) yield return null;
        Assert.AreEqual(oldPosition + Direction.Left, sut.CurrentCell);
        oldPosition = sut.CurrentCell;
        yield return new WaitForSeconds(sut.Delay);
        yield return null;

        while (sut.IsMoving) yield return null;
        Assert.AreEqual(oldPosition + Direction.Right, sut.CurrentCell);
        yield return new WaitForSeconds(sut.Delay);
        yield return null;

        // The move route is done and shouldn't loop
        Assert.False(sut.IsMoving);
    }

    [UnityTest, Order(3)]
    public IEnumerator Move_route_loops_if_set()
    {
        // Arrange
        sut.NeverMoves = true; // hold still until we're ready
        while (sut.IsMoving) yield return null; // finish any current movement
        sut.MovesRandomly = false;
        sut.MoveRouteLoops = true;
        float timeElapsed = 0;

        // Act
        sut.NeverMoves = false;
        yield return new WaitForSeconds(3); // give the first loop enough time to finish

        // Assert
        while (timeElapsed < sut.Delay + .5f)
        {
            if (sut.IsMoving == true)
            {
                Assert.Pass();
                yield break;
            }
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        Assert.Fail();
    }

    [UnityTest, Order(4)]
    public IEnumerator Turns_to_player_on_interact()
    {
        // Arrange
        sut.Turner.Turn(Direction.Left);

        // Act
        sut.Interact();
        yield return null;

        // Assert
        Assert.AreEqual(Direction.Right, sut.Facing);
    }

    [UnityTest, Order(5)]
    public IEnumerator Interaction_cutscene_triggers()
    {
        // Arrange

        // Act

        // Assert
        yield return new WaitForSeconds(.25f);
        Assert.AreEqual(GameState.Dialogue, Game.State);
    }

    [UnityTest, Order(6)]
    public IEnumerator Doesnt_move_during_scene()
    {
        // Arrange
        while (sut.IsMoving) yield return null; // finish any current movement

        // Act

        // Assert
        float timeElapsed = 0;
        while (timeElapsed <= sut.Delay + .1f)
        {
            if (sut.IsMoving)
                Assert.Fail();
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Close the dialogue window
        DialogueWindow window = GameObject.FindObjectOfType<DialogueWindow>();
        window.GoToNextLine();
        yield return null;
        window.GoToNextLine();
        yield return null;
        window.GoToNextLine();
        yield return null;
    }
}
