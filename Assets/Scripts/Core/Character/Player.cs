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

        public void CheckCurrentCell()
        {
            if (Game.Manager.Map.Exits.ContainsKey(CurrentCell))
            {
                Exit exit = Game.Manager.Map.Exits[CurrentCell];
                exit.TeleportPlayer();
            }
        }

    }
}
