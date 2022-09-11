using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Battle
{
    public class ActorStats : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hp;
        private BattleControl battleControl;
        private Actor actor;

        private void Awake()
        {
            battleControl = FindObjectOfType<BattleControl>();
            actor = GetComponentInParent<Actor>();
        }

        private void Update()
        {
            if (!battleControl.SetupComplete)
                return;

            hp.text = $"{actor.Stats.HP}/{actor.Stats.MaxHP}";

            if (actor.Stats.HP / actor.Stats.MaxHP < .2f)
                hp.color = Color.yellow;
            else
                hp.color = Color.white;
        }
    }
}
