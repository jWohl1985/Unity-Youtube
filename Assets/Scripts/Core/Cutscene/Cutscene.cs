using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Cutscene : MonoBehaviour, ITriggerTouch
    {
        [SerializeField] private TriggerType trigger = TriggerType.Autoplay;
        [SerializeField] private bool oneTimeOnly = false;

        [SerializeReference]
        private List<ICutsceneCommand> commands = new List<ICutsceneCommand>();
        private bool isStarted = false;

        public Vector2Int Cell => Game.World.Map.Grid.GetCell2D(this.gameObject);
        public IReadOnlyList<ICutsceneCommand> Commands => commands;
        public bool HasBeenPlayed { get; set; } = false;

        private void Start()
        {
            if (trigger == TriggerType.Touch)
                Game.World.Map.TriggerCells.Add(Cell, this);
        }

        private void Update()
        {
            if (trigger == TriggerType.Autoplay && !isStarted)
                TryPlay();
        }

        public void TryPlay()
        {
            if (oneTimeOnly && HasBeenPlayed)
                return;

            if (Game.Cutscenes.TryPlayCutscene(this))
                isStarted = true;
        }

        public void Trigger() => TryPlay();
        public void InsertCommand(int index, ICutsceneCommand command) => commands.Insert(index, command);
        public void RemoveAt(int i) => commands.RemoveAt(i);
        public void SwapCommands(int i, int j) => (commands[i], commands[j]) = (commands[j], commands[i]);

    }
}
