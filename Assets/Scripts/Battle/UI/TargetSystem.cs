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
        
        public void GetTarget(TargetType targetType, TargetDefault targetDefault)
        {
            IsFindingTarget = true;
            selectedTargets = new List<Actor>();

            switch(targetType)
            {
                case (TargetType.AnySingle):
                    GetAnySingleTarget(targetDefault);
                    break;
                default:
                    Debug.LogWarning("Target type not implemented yet!");
                    break;
            }
        }

        private void GetAnySingleTarget(TargetDefault targetDefault)
        {
            validTargets = battleControl.TurnOrder;
            Actor requestingActor = battleControl.TurnOrder[battleControl.TurnNumber];

            if (targetDefault == TargetDefault.Ally)
                Instantiate(targetSelectorPrefab, requestingActor.transform);
            else
                Instantiate(targetSelectorPrefab, battleControl.Enemies[0].transform);
        }

        public void Accept(TargetSelector targetSelector)
        {
            selectedTargets.Add(targetSelector.GetComponentInParent<Actor>());
            Destroy(targetSelector.gameObject);
            IsFindingTarget = false;
        }
    }
}
