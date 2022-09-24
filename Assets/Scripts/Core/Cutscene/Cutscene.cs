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

        public void InsertCommand(int index, ICutsceneCommand command) => commands.Insert(index, command);
        public void RemoveAt(int i) => commands.RemoveAt(i);
        public void SwapCommands(int i, int j)
        {
            (commands[i], commands[j]) = (commands[j], commands[i]);
        }
    }
}
