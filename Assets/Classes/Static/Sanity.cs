using System;
using Classes.Interfaces;

namespace Classes.Static
{
    [Serializable]
    public class Sanity : IUpdateable
    {
        public float BaseSanityDrop = 0.3f;
        public float StartingSanity = 100f;

        public int CurrentFearLevel = 0;
        
        public float CurrentSanity { get; private set; }
        public float CurrentSanityDrop { get; private set; }
        
        public void Start()
        {
            CurrentSanity = StartingSanity;
        }

        public void Update(float deltaTime)
        {
            CurrentSanityDrop = BaseSanityDrop + BaseSanityDrop * CurrentFearLevel;
            
            if (CurrentSanity > 0f)
            {
                CurrentSanity -= CurrentSanityDrop * deltaTime;
            }

            if (CurrentSanity > StartingSanity)
            {
                CurrentSanity = StartingSanity;
            }
        }
    }
}