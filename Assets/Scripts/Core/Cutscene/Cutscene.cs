using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Cutscene : MonoBehaviour
    {
        [SerializeField] private bool autoplay = false;

        [SerializeReference]
        private List<ICutsceneCommand> commands = new List<ICutsceneCommand>();

        private bool isfinished = false;
        private bool isStarted = false;

        public IReadOnlyList<ICutsceneCommand> Commands => commands;
        public bool IsFinished
        {
            get => isfinished;
            set
            {
                isfinished = value;
                if (value == true)
                    Destroy(this.gameObject);
            }
        }

        private void Update()
        {
            if (autoplay && !isStarted)
                Play();
        }

        public void Play()
        {
            if (Game.Manager.TryPlayCutscene(this))
                isStarted = true;
        }

        public void AddCommand(ICutsceneCommand command) => commands.Add(command);
    }
}
