using GameObjects;
using Handlers;
using UnityEngine;

namespace Controllers
{
    public class SecurityCamerasController : Singleton<SecurityCamerasController>
    {
        public GameObject PlayerCamera;
        public AudioSource FocusCamSound;

        public GameObject CurrentCamera;

        private void Start()
        {
            EventHandler.Instance.OnPlayerFocusChange += ToggleCams;
        }

        public void ChangeCam(GameObject newCam)
        {
            if (CurrentCamera == newCam)
            {
                return;
            }

            CurrentCamera.GetComponent<Camera>().enabled = false;
            newCam.GetComponent<Camera>().enabled = true;

            CurrentCamera.GetComponent<SecurityCamera>().AudioListener.SetActive(false);
            newCam.GetComponent<SecurityCamera>().AudioListener.SetActive(true);

            CurrentCamera = newCam;
        }

        private void ToggleCams(bool isFocused)
        {
            PlayerCamera.GetComponent<Camera>().enabled = !isFocused;
            CurrentCamera.GetComponent<Camera>().enabled = isFocused;

            CurrentCamera.GetComponent<SecurityCamera>().AudioListener.SetActive(isFocused);

            var focus = PlayerFocusController.Instance;

            AudioListener.volume = isFocused ? focus.FocusedAudioVolume : focus.UnfocusedAudioVolume;

            if (isFocused)
            {
                FocusCamSound.Play();
            }
        }
    }
}