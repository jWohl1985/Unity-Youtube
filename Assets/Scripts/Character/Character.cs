using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{
    public CharacterMover Move { get; private set; }
    public CharacterTurner Turn { get; private set; }
    public CharacterAnimator Animator { get; private set; }
    public bool IsMoving => Move.IsMoving;
    public Vector2Int Facing => Turn.Facing;
    public Vector2Int CurrentCell => Game.Map.Grid.GetCell2D(this.gameObject);

    protected virtual void Awake()
    {
        Move = new CharacterMover(this);
        Turn = new CharacterTurner();
        Animator = new CharacterAnimator(this);
    }

    protected virtual void Start()
    {
        Vector2Int currentCell = Game.Map.Grid.GetCell2D(this.gameObject);
        transform.position = Game.Map.Grid.GetCellCenter2D(currentCell);
        Game.Map.OccupiedCells.Add(currentCell, this);
    }

    protected virtual void Update()
    {
        Animator.ChooseLayer();
        Animator.UpdateParameters();
    }
}
