using UnityEngine;

namespace Handlers
{
    public class UIHandler : Singleton<UIHandler>
    {
        public GameObject PlayerUICanvas;
        public GameObject CameraUICanvas;
        public GameObject CellphoneUICanvas;

        public void TogglePlayerUI(bool isActive)
        {
            PlayerUICanvas.SetActive(isActive);
        }
        
        public void ToggleCameraUI(bool isActive)
        {
            CameraUICanvas.SetActive(isActive);
        }

        public void ToggleCellphoneUI(bool isActive)
        {
            CellphoneUICanvas.SetActive(isActive);
        }
    }
}