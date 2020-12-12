using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace MiniGames
{
    public class ClickTheSphere : MiniGame
    {
        public GameObject Sphere;
        public AudioSource ShrinkSound;
        
        public int StartingClicksNeeded = 15;

        private int _currentClicksNeeded = 15;
    
        public void OnSphereClick()
        {
            _currentClicksNeeded--;

            var newScale = (float)_currentClicksNeeded / StartingClicksNeeded;
            
            ShrinkSound.Play();
            ShrinkSound.pitch = newScale;
            
            Sphere.transform.localScale = Vector3.one * newScale;

            if (_currentClicksNeeded == 0)
            {
                FinishMiniGame();
            }
        }
    }
}
