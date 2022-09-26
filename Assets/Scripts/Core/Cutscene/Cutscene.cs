using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Cutscene : MonoBehaviour, ITriggerTouch
    {
        [SerializeField] private TriggerType trigger = TriggerType.Autoplay;

        [SerializeReference]
        private List<ICutsceneCommand> commands = new List<ICutsceneCommand>();
        private bool isFinished = false;
        private bool isStarted = false;

        public Vector2Int Cell => Game.Manager.Map.Grid.GetCell2D(this.gameObject);
        public IReadOnlyList<ICutsceneCommand> Commands => commands;
        public bool IsFinished
        {
            get => isFinished;
            set
            {
                isFinished = value;
                if (value == true)
                    Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            if (trigger == TriggerType.Touch)
                Game.Manager.Map.TriggerCells.Add(Cell, this);
        }

        private void Update()
        {
            if (trigger == TriggerType.Autoplay && !isStarted)
                Play();
        }

        public void Play()
        {
            if (Game.Manager.TryPlayCutscene(this))
                isStarted = true;
        }

        public void Trigger() => Play();

        public void InsertCommand(int index, ICutsceneCommand command) => commands.Insert(index, command);
        public void RemoveAt(int i) => commands.RemoveAt(i);
        public void SwapCommands(int i, int j)
        {
            (commands[i], commands[j]) = (commands[j], commands[i]);
        }
    }
}
