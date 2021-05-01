using System;

namespace Classes
{
    [Serializable]
    public class GameTime
    {
        public int Hour;
        public float Minutes;

        public GameTime(int hour, float minutes)
        {
            Hour = hour % 24;
            Minutes = minutes % 60;
        }

        public GameTime(GameTime gameTime)
        {
            Hour = gameTime.Hour;
            Minutes = gameTime.Minutes;
        }

        public void AddMinutes(float minutes)
        {
            var sumMinutes = Minutes + minutes;

            Minutes = sumMinutes % 60;

            Hour += (int)sumMinutes / 60;
            Hour %= 24;
        }

        public void AddHours(int hours)
        {
            Hour += hours % 24;
        }

        public bool IsEqual(GameTime time)
        {
            return Hour == time.Hour && (int)Minutes == (int)time.Minutes;
        }

        public bool IsGreaterOrEqual(GameTime time)
        {
            return Hour >= time.Hour && (int)Minutes >= (int)time.Minutes;
        }

        public override string ToString()
        {
            var hour = Hour.ToString();
            var minutes = ((int)Minutes).ToString();

            if (Hour < 10)
            {
                hour = "0" + hour;
            }

            if (Minutes < 10)
            {
                minutes = "0" + minutes;
            }

            return $"{hour}:{minutes}";
        }
    }
}