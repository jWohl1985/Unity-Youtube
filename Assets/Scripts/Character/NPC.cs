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

    [SerializeField] private ScriptableObject interaction;
    [SerializeField] private List<Dir> moveRoute = new List<Dir>();
    [SerializeField] private float delay = 0f;
    [SerializeField] private bool neverMoves = false;
    [SerializeField] private bool movesRandomly = false;
    [SerializeField] private bool moveRouteLoops = false;

    public bool NeverMoves
    {
        get => neverMoves;
        set => neverMoves = value;
    }

    public bool MovesRandomly
    {
        get => movesRandomly;
        set => movesRandomly = value;
    }

    public bool MoveRouteLoops
    {
        get => moveRouteLoops;
        set
        {
            currentMoveRouteIndex = 0;
            moveRouteLoops = value;
        }
    }

    public float Delay
    {
        get => delay;
        set => delay = Mathf.Clamp(value, 0, 30.0f);
    }

    private int currentMoveRouteIndex = 0;
    private float timeElapsed = 0;

    public ScriptableObject Interaction => interaction;

    public void Interact()
    {
        if(interaction is DialogueScene scene)
        {
            Vector2Int currentFacing = Facing;
            Turn.TurnToPlayer();
            Game.StartDialogue(scene);
        }
    }

    protected override void Update()
    {
        base.Update();

        if (Game.State != GameState.World)
            return;

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
