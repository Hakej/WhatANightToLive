using UnityEngine;

namespace MiniGames
{
    public class ClickTheSphere : MiniGame
    {
        public GameObject Sphere;
        
        public int StartingClicksNeeded = 15;

        private int _currentClicksNeeded = 15;
    
        public void OnSphereClick()
        {
            _currentClicksNeeded--;

            var newScale = (float)_currentClicksNeeded / StartingClicksNeeded;
            
            Sphere.transform.localScale = new Vector3(newScale, newScale, newScale);

            if (_currentClicksNeeded == 0)
            {
                FinishMiniGame();
            }
        }
    }
}
