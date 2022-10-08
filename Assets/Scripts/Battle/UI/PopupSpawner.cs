using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class PopupSpawner : MonoBehaviour
    {
        [SerializeField] private DamagePopup popupPrefab;

        private BattleControl battleControl;

        private void Awake()
        {
            battleControl = GetComponentInParent<BattleControl>();
        }

        void Start()
        {
            foreach(Actor actor in battleControl.TurnOrder)
            {
                actor.TookDamage += SpawnPopup;
            }
        }

        private void SpawnPopup(Actor actor, int amount)
        {
            Transform canvas = actor.GetComponentInChildren<Canvas>().transform;
            DamagePopup popup = Instantiate(popupPrefab, canvas);

            if (amount <= 0)
                popup.SetText("Miss!");
            else
                popup.SetText(amount.ToString());
        }
    }
}
