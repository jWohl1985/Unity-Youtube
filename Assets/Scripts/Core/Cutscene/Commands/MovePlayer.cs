using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class MovePlayer : ICutsceneCommand
    {
        [SerializeField] private float speed;
        [SerializeField] private List<Dir> route;
        
        public bool IsFinished { get; set; }

        public IEnumerator Co_Execute()
        {
            foreach (Dir dir in route)
            {
                Vector2Int direction = dir switch
                {
                    Dir.Up => new Vector2Int(0, 1),
                    Dir.Down => new Vector2Int(0, -1),
                    Dir.Left => new Vector2Int(-1, 0),
                    Dir.Right => new Vector2Int(1, 0),
                    _ => new Vector2Int(0, 0),
                };

                Game.Player.Movement.TryMove(direction);
                yield return null;

                while (Game.Player.IsMoving)
                    yield return null;
            }

            IsFinished = true;
        }

        public override string ToString() => "Move Player";
    }
}
