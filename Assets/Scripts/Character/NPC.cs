using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character, IInteractable
{
    private enum Dir
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3,
    }

    [SerializeField] private Interaction interaction;
    [SerializeField] private List<Dir> moveRoute = new List<Dir>();
    [SerializeField] private float delay = 0f;
    [SerializeField] private bool neverMoves = false;
    [SerializeField] private bool movesRandomly = false;
    [SerializeField] private bool moveRouteLoops = false;

    private int currentMoveRouteIndex = 0;
    private float timeElapsed = 0;

    public Interaction Interaction => interaction;

    public void Interact()
    {
        Interaction.StartInteraction();
    }

    protected override void Update()
    {
        base.Update();

        if (neverMoves || IsMoving)
            return;

        timeElapsed += Time.deltaTime;

        if (timeElapsed < delay)
            return;

        timeElapsed = 0;

        if (movesRandomly)
        {
            MoveInRandomDirection();
            return;
        }

        ExecuteMoveRoute();
    }

    private void MoveInRandomDirection()
    {
        int random = Random.Range(0, 4);
        Vector2Int moveDirection = random switch
        {
            0 => Direction.Left,
            1 => Direction.Right,
            2 => Direction.Up,
            3 => Direction.Down,
            _ => new Vector2Int(0, 0)
        };

        Move.TryMove(moveDirection);
    }

    private void ExecuteMoveRoute()
    {
        if (currentMoveRouteIndex >= moveRoute.Count)
            return;

        Dir direction = moveRoute[currentMoveRouteIndex];
        Vector2Int moveDirection = direction switch
        {
            Dir.Left => Direction.Left,
            Dir.Right => Direction.Right,
            Dir.Up => Direction.Up,
            Dir.Down => Direction.Down,
            _ => new Vector2Int(0, 0)
        };

        Move.TryMove(moveDirection);
        currentMoveRouteIndex++;

        if (moveRouteLoops)
            currentMoveRouteIndex = currentMoveRouteIndex % moveRoute.Count;
    }
}
