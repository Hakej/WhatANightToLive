using System;

namespace Classes
{
    [Serializable]
    public class Time
    {
        public int Hour;
        public float Minutes;

        public Time(int hour, float minutes)
        {
            Hour = hour;
            Minutes = minutes;
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