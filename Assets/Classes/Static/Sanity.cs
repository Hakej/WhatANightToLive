using Classes.Interfaces;

namespace Classes.Static
{
    public class Sanity : IUpdateable
    {
        public float CurrentSanity;

        private float _baseSanityDrop;
        private float _startingSanity;
        private float _currentSanityDrop;
        
        public Sanity(float startingSanity, float baseSanityDrop)
        {
            CurrentSanity = startingSanity;
            
            _startingSanity = startingSanity;
            _baseSanityDrop = baseSanityDrop;
            _currentSanityDrop = baseSanityDrop;
        }

        public void Update(float deltaTime)
        {
            if (CurrentSanity > 0f)
            {
                CurrentSanity -= _currentSanityDrop * deltaTime;
            }
        }
    }
}