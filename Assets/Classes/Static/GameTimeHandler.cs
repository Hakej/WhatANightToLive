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

        private GameTime _sunriseStartGameTime;

        private void Start()
        {
            CurrentGameTime = new GameTime(StartingGameTime);

            var sunriseMinutes = Sunrise.Instance.SunriseLengthInGameMinutes;

            var winHours = WinningGameTime.Hour == 0 ? 24 : WinningGameTime.Hour;
            var winMinutes = WinningGameTime.Minutes == 0 ? 60 : WinningGameTime.Minutes;

            var hours = winHours - (int) Math.Ceiling(sunriseMinutes / 60);
            var minutes = winMinutes - sunriseMinutes % 60;

            _sunriseStartGameTime = new GameTime(hours, minutes);
        }

        public void Update()
        {
            if (CurrentGameTime.IsEqual(WinningGameTime))
            {
                EventHandler.Instance.Win();
                return;
            }

            if (CurrentGameTime.IsEqual(_sunriseStartGameTime))
            {
                Sunrise.Instance.StartSunrise();
            }

            CurrentGameTime.AddMinutes(Time.deltaTime);
        }
    }
}