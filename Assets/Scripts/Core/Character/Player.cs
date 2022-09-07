using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Player : Character
    {
        private InputHandler InputHandler;

        protected override void Awake()
        {
            base.Awake();
            InputHandler = new InputHandler(this);
        }

        protected override void Start()
        {
            base.Start();
        }


        protected override void Update()
        {
            base.Update();
            InputHandler.CheckInput();
        }

        public void OnMovementFinished()
        {
            if (Map.Transfers.ContainsKey(CurrentCell))
            {
                Transfer transfer = Map.Transfers[CurrentCell];
                transfer.TeleportPlayer();
                return;
            }

            if (Map.Region != null)
                Map.Region.CheckForEncounter(Map);
        }

    }
}
