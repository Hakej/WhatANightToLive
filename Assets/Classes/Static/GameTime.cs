using System;
using Classes.Interfaces;

namespace Classes.Static
{
    [Serializable]
    public class GameTime : IUpdateable
    {
        public Time CurrentTime;

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
    }
}