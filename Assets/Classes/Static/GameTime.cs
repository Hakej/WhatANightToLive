using Classes.Interfaces;

namespace Classes.Static
{
    public class GameTime : IUpdateable
    {
        public Time CurrentTime;

        private readonly Time _winningTime;
        
        public GameTime(Time time, Time winningTime)
        {
            CurrentTime = time;
            _winningTime = winningTime;
        }

        public void Update(float deltaTime)
        {
            if (CurrentTime.Hour == _winningTime.Hour)
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