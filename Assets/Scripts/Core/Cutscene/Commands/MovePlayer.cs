using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class MovePlayer : ICutsceneCommand
    {
        [SerializeField] private List<Dir> route;
        [SerializeField] private float speed;

        public bool IsFinished { get; private set; }

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

                Game.Manager.Player.Movement.TryMove(direction);
                yield return null;

                while (Game.Manager.Player.IsMoving)
                    yield return null;
            }

            IsFinished = true;
        }

        public override string ToString() => "Move Player";
    }
}
