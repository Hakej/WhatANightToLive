using System;
using Classes.Interfaces;
using UnityEngine;
using EventHandler = Handlers.EventHandler;

namespace Classes.Static
{
    [Serializable]
    public class GameTime : IUpdateable
    {
        public Time CurrentTime;
        public Time SunriseStartTime;

        public Time WinningTime;

        public void Start()
        {
        }
        
        public void Update(float deltaTime)
        {
            if (CurrentTime.Hour == WinningTime.Hour)
            {
                EventHandler.Instance.Win();
                return;
            }

            if (IsCurrentTimeEqual(SunriseStartTime))
            {
                Sunrise.Instance.StartSunrise();
            }
            
            CurrentTime.Minutes += deltaTime;
            
            if (CurrentTime.Minutes < 60)
            {
                return;
            }
            
            CurrentTime.Minutes = 0f;
            CurrentTime.Hour++;
            
            if (CurrentTime.Hour >= 24)
            {
                CurrentTime.Hour = 0;
            }
        }

        public bool IsCurrentTimeEqual(Time time)
        {
            return CurrentTime.Hour == time.Hour && (int) CurrentTime.Minutes == (int) time.Minutes;
        }
    }
}