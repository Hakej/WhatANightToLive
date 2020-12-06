using Classes.Interfaces;
using Handlers;
using UnityEngine;

namespace Classes.Static
{
    public class Sanity : IUpdateable
    {
        public float CurrentSanity;

        private float _baseSanityDrop;
        private float _startingSanity;
        private float _currentSanityDrop;
        private float _fearSanityDropMultiplier;
        
        public Sanity(float startingSanity, float baseSanityDrop, float fearSanityDropMultiplier)
        {
            CurrentSanity = startingSanity;
            
            _startingSanity = startingSanity;
            _baseSanityDrop = baseSanityDrop;
            _currentSanityDrop = baseSanityDrop;
            _fearSanityDropMultiplier = fearSanityDropMultiplier;
        }

        public void Update(float deltaTime)
        {
            _currentSanityDrop = _baseSanityDrop * _fearSanityDropMultiplier;
            
            if (CurrentSanity > 0f)
            {
                CurrentSanity -= _currentSanityDrop * deltaTime;
            }
        }
    }
}