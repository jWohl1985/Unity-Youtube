using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [System.Serializable]
    public class MoveCharacter : ICutsceneCommand
    {
        [SerializeField] private Character character;
        [SerializeField] private float speed;
        [SerializeField] private List<Dir> route = new List<Dir>();

        public bool IsFinished { get; private set; }

        public IEnumerator Co_Execute()
        {
            Debug.Log("Here");
            foreach(Dir dir in route)
            {
                Vector2Int direction = dir switch
                {
                    Dir.Up => new Vector2Int(0, 1),
                    Dir.Down => new Vector2Int(0, -1),
                    Dir.Left => new Vector2Int(-1, 0),
                    Dir.Right => new Vector2Int(1, 0),
                    _ => new Vector2Int(0, 0),
                };

                character.Movement.TryMove(direction);
                yield return null;

                while (character.IsMoving)
                    yield return null;
            }

            IsFinished = true;
        }
    }
}
