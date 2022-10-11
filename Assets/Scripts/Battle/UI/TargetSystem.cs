using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class TargetSystem : MonoBehaviour
    {
        [SerializeField] private TargetSelector targetSelectorPrefab;

        private BattleControl battleControl;
        private IReadOnlyList<Actor> validTargets;
        private List<Actor> selectedTargets;

        public IReadOnlyList<Actor> ValidTargets => validTargets;
        public IReadOnlyList<Actor> SelectedTargets => selectedTargets;

        public bool IsFindingTarget { get; private set; }

        private void Awake()
        {
            battleControl = GetComponentInParent<BattleControl>();
        }

        private void Start()
        {
            foreach (Ally ally in battleControl.Allies)
                ally.NeedTarget += GetTarget;
        }

        
        private void GetTarget(Actor requestingActor, TargetType targetType, TargetDefault targetDefault)
        {
            IsFindingTarget = true;
            selectedTargets = new List<Actor>();

            switch(targetType)
            {
                case (TargetType.AnySingle):
                    StartCoroutine(Co_GetAnySingleTarget(requestingActor, targetDefault));
                    break;
                default:
                    Debug.LogWarning("Target type not implemented yet!");
                    break;
            }
        }

        private IEnumerator Co_GetAnySingleTarget(Actor requestingActor, TargetDefault targetDefault)
        {
            validTargets = battleControl.TurnOrder;

            TargetSelector targetSelector;

            if (targetDefault == TargetDefault.Ally)
                targetSelector = Instantiate(targetSelectorPrefab, requestingActor.transform);
            else
                targetSelector = Instantiate(targetSelectorPrefab, battleControl.Enemies[0].transform);

            while (IsFindingTarget)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    selectedTargets.Add(targetSelector.GetComponentInParent<Actor>());
                    break;
                }
                yield return null;
            }

            IsFindingTarget = false;
        }
    }
}
