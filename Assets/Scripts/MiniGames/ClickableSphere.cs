using UnityEngine;

namespace MiniGames
{
    public class ClickableSphere : MonoBehaviour
    {
        public ClickTheSphere ClickTheSphere;
        
        private void OnMouseDown()
        {
            ClickTheSphere.OnSphereClick();
        }
    }
}
