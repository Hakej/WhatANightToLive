using System;
using UnityEngine;
using EventHandler = Handlers.EventHandler;

namespace Classes.Static
{
    [Serializable]
    public class GameTimeHandler : Singleton<GameTimeHandler>
    {
        public GameTime StartingGameTime;
        public GameTime WinningGameTime;

        [HideInInspector]
        public GameTime CurrentGameTime;

        private void Start()
        {
            CurrentGameTime = new GameTime(StartingGameTime);

            var winHours = WinningGameTime.Hour == 0 ? 24 : WinningGameTime.Hour;
            var winMinutes = WinningGameTime.Minutes == 0 ? 60 : WinningGameTime.Minutes;
        }

        public void Update()
        {
            if (CurrentGameTime.IsEqual(WinningGameTime))
            {
                EventHandler.Instance.Win();
                return;
            }

            CurrentGameTime.AddMinutes(Time.deltaTime);
        }
    }
}