using GameObjects;
using Handlers;
using UnityEngine;

namespace Controllers
{
    public class SecurityCamerasController : Singleton<SecurityCamerasController>
    {
        public GameObject PlayerCamera;

        public GameObject CurrentCamera;

        private void Start()
        {
            //EventHandler.Instance.OnPlayerFocusChange += ToggleCams;
        }

        public void ChangeCam(GameObject newCam)
        {
            if (CurrentCamera == newCam)
            {
                return;
            }

            CurrentCamera.GetComponent<Camera>().enabled = false;
            newCam.GetComponent<Camera>().enabled = true;

            CurrentCamera = newCam;
        }

        private void ToggleCams(bool isFocused)
        {
            PlayerCamera.GetComponent<Camera>().enabled = !isFocused;
            CurrentCamera.GetComponent<Camera>().enabled = isFocused;

            var focus = LaptopController.Instance;
        }
    }
}