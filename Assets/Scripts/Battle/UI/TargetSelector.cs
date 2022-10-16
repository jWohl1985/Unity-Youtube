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
                FindSelectionInDirection(Dir.Up);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                FindSelectionInDirection(Dir.Down);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                FindSelectionInDirection(Dir.Left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                FindSelectionInDirection(Dir.Right);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if ((Vector2)selectorTransform.localPosition != selectorOffset)
                    return;

                targetSystem.Accept(this);
            }

        }

        private void FindSelectionInDirection(Dir dir)
        {
            Actor actorFound = FindActorInDirection(dir);
            if (actorFound == null)
                return;

            CurrentSelection = actorFound.GetComponent<Transform>();
            selectorTransform.SetParent(CurrentSelection);
        }

        private Actor FindActorInDirection(Dir direction)
        {
            List<Actor> actorsFound;
            switch (direction)
            {
                case (Dir.Up):
                    return targets
                        .Where(actor => actor.transform.position.x == CurrentSelection.transform.position.x)
                        .Where(actor => actor.transform.position.y > CurrentSelection.transform.position.y)
                        .OrderBy(actor => actor.transform.position.y)
                        .FirstOrDefault();

                case (Dir.Down):
                    return targets
                        .Where(actor => actor.transform.position.x == CurrentSelection.transform.position.x)
                        .Where(actor => actor.transform.position.y < CurrentSelection.transform.position.y)
                        .OrderByDescending(actor => actor.transform.position.y)
                        .FirstOrDefault();

                case (Dir.Left):
                    actorsFound = targets
                        .Where(actor => actor.transform.position.x < CurrentSelection.transform.position.x)
                        .OrderByDescending(actor => actor.transform.position.x)
                        .ToList();

                    if (actorsFound.Count == 0)
                        return null;

                    float highestX = actorsFound.First().transform.position.x;

                    return FindClosestActor(actorsFound.Where(actor => actor.transform.position.x == highestX));

                case (Dir.Right):
                    actorsFound = targets
                        .Where(actor => actor.transform.position.x > CurrentSelection.transform.position.x)
                        .OrderBy(actor => actor.transform.position.x)
                        .ToList();

                    if (actorsFound.Count == 0)
                        return null;

                    float lowestX = actorsFound.First().transform.position.x;

                    return FindClosestActor(actorsFound.Where(actor => actor.transform.position.x == lowestX));
                default:
                    return null;
            }
        }

        private Actor FindClosestActor(IEnumerable<Actor> actors)
        {
            float closestDistance = float.MaxValue;
            Actor closestActor = actors.First();

            foreach(Actor actor in actors)
            {
                float distance = ((Vector2)actor.transform.position - (Vector2)CurrentSelection.transform.position).magnitude;
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestActor = actor;
                }
            }

            return closestActor;
        }
    }
}
