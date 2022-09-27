using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [System.Serializable]
    public class Wait : ICutsceneCommand
    {
        [SerializeField] private float seconds = 0;

        public bool IsFinished { get; set; }

        public IEnumerator Co_Execute()
        {
            yield return new WaitForSeconds(seconds);
            IsFinished = true;
        }

        public override string ToString() => "Wait For Seconds";
    }
}
