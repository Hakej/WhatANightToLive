using Classes.Interfaces;

namespace Classes.Static
{
    public class GameTime : IUpdateable
    {
        private int _hour;
        private float _minutes;

        public GameTime(int hour, float minutes)
        {
            _hour = hour;
            _minutes = minutes;
        }

        public override string ToString()
        {
            var hour = _hour.ToString();
            var minutes = ((int)_minutes).ToString();

            if (_hour < 10)
            {
                hour = "0" + hour;
            }

            if (_minutes < 10)
            {
                minutes = "0" + minutes;
            }
            
            return $"{hour}:{minutes}";
        }

        public void Update(float deltaTime)
        {
            _minutes += deltaTime;

            if (!(_minutes >= 60))
            {
                return;
            }

            _hour++;
            _minutes = 0f;
            
            if (_hour >= 24)
            {
                _hour = 0;
            }
        }
    }
}