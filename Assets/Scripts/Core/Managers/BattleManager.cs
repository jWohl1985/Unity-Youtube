using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;

namespace Core
{
    public class BattleManager
    {
        private StateManager stateManager;
        private GameObject battleTransition;

        public BattleManager(StateManager stateManager)
        {
            this.stateManager = stateManager;
            this.battleTransition = Resources.Load<GameObject>("Transitions/BattleTransition");
        }

        public void StartBattle(EnemyPack pack) => Game.Player.StartCoroutine(Co_StartBattle(pack));

        private IEnumerator Co_StartBattle(EnemyPack pack)
        {
            stateManager.SetState(GameState.Battle);
            BattleControl.EnemyPack = pack;
            Animator animator = PlayTransition();
            while (animator.IsAnimating()) yield return null;
            SceneLoader.LoadBattleScene();
        }

        public void EndBattle()
        {
            SceneLoader.ReloadSavedSceneAfterBattle();
            stateManager.SetState(GameState.World);
        }

        private Animator PlayTransition()
        {
            Vector3 position = Game.Player.transform.position;
            Animator animator = GameObject.Instantiate(battleTransition, position, Quaternion.identity).GetComponent<Animator>();
            return animator;
        }
    }
}
