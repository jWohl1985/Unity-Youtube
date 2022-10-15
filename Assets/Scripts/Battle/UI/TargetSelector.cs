using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using System.Linq;

namespace Battle
{
    public class TargetSelector : MonoBehaviour
    {
        private TargetSystem targetSystem;
        private IReadOnlyList<Actor> targets;
        private Transform selectorTransform;
        private Transform CurrentSelection;

        private Vector2 selectorOffset = new Vector2(0, -1.75f);

        private const float MOVE_SPEED = 40f;


        private void Awake()
        {
            targetSystem = FindObjectOfType<TargetSystem>();
            selectorTransform = GetComponent<Transform>();
            CurrentSelection = GetComponentInParent<Actor>().GetComponent<Transform>();
        }

        private void Start()
        {
            targets = targetSystem.ValidTargets;
        }

        private void Update()
        {
            selectorTransform.localPosition = Vector2.MoveTowards(selectorTransform.localPosition, selectorOffset, MOVE_SPEED * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                StartCoroutine(Co_MoveUp());
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                StartCoroutine(Co_MoveDown());
            }

        }

        private IEnumerator Co_MoveUp()
        {
            List<Actor> actorsFound = FindActorInDirection(Dir.Up);
            if (actorsFound.Count == 0)
                yield break;

            CurrentSelection = actorsFound.First().GetComponent<Transform>();

            selectorTransform.SetParent(CurrentSelection);
        }

        private IEnumerator Co_MoveDown()
        {
            List<Actor> actorsFound = FindActorInDirection(Dir.Down);
            if (actorsFound.Count == 0)
                yield break;

            CurrentSelection = actorsFound.First().GetComponent<Transform>();

            selectorTransform.SetParent(CurrentSelection);
        }

        /*private IEnumerator Co_MoveLeft()
        {
            List<Actor> actorsFound = FindActorsInDirection();
        }

        private IEnumerator Co_MoveRight()
        {
            List<Actor> actorsFound = FindActorsInDirection();
        }*/

        private List<Actor> FindActorInDirection(Dir direction)
        {
            List<Actor> actorsFound;
            switch (direction)
            {
                case (Dir.Up):
                    actorsFound = targets
                        .Where(actor => actor.transform.position.x == CurrentSelection.transform.position.x)
                        .Where(actor => actor.transform.position.y > CurrentSelection.transform.position.y)
                        .OrderBy(actor => actor.transform.position.y)
                        .ToList();
                    return actorsFound;
                case (Dir.Down):
                    actorsFound = targets
                        .Where(actor => actor.transform.position.x == CurrentSelection.transform.position.x)
                        .Where(actor => actor.transform.position.y < CurrentSelection.transform.position.y)
                        .OrderByDescending(actor => actor.transform.position.y)
                        .ToList();
                    return actorsFound;
                default:
                    return null;
            }
        }
    }
}
