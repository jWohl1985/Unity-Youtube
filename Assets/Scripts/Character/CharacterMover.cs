using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover
{
    private Character character;
    private Transform transform;
    private const float TIME_TO_MOVE_ONE_SQUARE = .25f;

    public bool IsMoving { get; private set; } = false;

    public CharacterMover(Character character)
    {
        this.character = character;
        this.transform = character.transform;
    }

    public void TryMove(Vector2Int direction)
    {
        if (IsMoving || !direction.IsBasic()) 
            return;

        character.Turn.Turn(direction);
        Vector2Int targetCell = character.CurrentCell + direction;

        if (CanMoveIntoCell(targetCell, direction))
        {
            Game.Map.OccupiedCells.Add(character.CurrentCell + direction, character);
            Game.Map.OccupiedCells.Remove(character.CurrentCell);
            character.StartCoroutine(Co_Move(direction));
        }
        
    }

    private bool CanMoveIntoCell(Vector2Int targetCell, Vector2Int direction)
    {
        if (IsCellOccupied(targetCell))
        {
            return false;
        }

        Ray2D ray = new Ray2D(character.CurrentCell.Center2D(), direction);
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 2.0f);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.distance < Game.Map.Grid.cellSize.x)
            {
                return false;
            }
        }

        return true;
    }

    private bool IsCellOccupied(Vector2Int cell)
    {
        return Game.Map.OccupiedCells.ContainsKey(cell);
    }

    private IEnumerator Co_Move(Vector2Int direction)
    {
        IsMoving = true;

        Vector2 startingPosition = character.CurrentCell.Center2D();
        Vector2 endingPosition = (character.CurrentCell + direction).Center2D();

        float elapsedTime = 0;

        while ((Vector2)transform.position != endingPosition)
        {
            transform.position = Vector2.Lerp(startingPosition, endingPosition, elapsedTime / TIME_TO_MOVE_ONE_SQUARE);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endingPosition;
        IsMoving = false;
    }
}
