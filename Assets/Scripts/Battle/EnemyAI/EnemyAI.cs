using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public abstract class EnemyAI : MonoBehaviour
    {
        public abstract ICommand ChooseAction();
    }
}
