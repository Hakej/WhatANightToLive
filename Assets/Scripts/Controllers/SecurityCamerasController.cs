using Classes;
using Handlers;
using UnityEngine;

namespace Controllers
{
    public class SecurityCamerasController : Singleton<SecurityCamerasController>
    {
        public GameObject PlayerCamera;
        public AudioSource FocusCamSound;
        public string SecurityCamTag;

        public LoopingList<GameObject> Cameras;

        private void Start()
        {
            Cameras = new LoopingList<GameObject>();

            var cameras = GameObject.FindGameObjectsWithTag(SecurityCamTag);

            foreach (var camera in cameras) Cameras.Add(camera);

            EventHandler.Instance.OnPlayerFocusChangeStop += ToggleCams;
        }

        public void NextCamera()
        {
            ChangeCam(Cameras.Current, Cameras.MoveNext);
        }

        public void PreviousCamera()
        {
            ChangeCam(Cameras.Current, Cameras.MovePrevious);
        }

        private void ChangeCam(GameObject oldCam, GameObject newCam)
        {
            oldCam.GetComponent<Camera>().enabled = false;
            newCam.GetComponent<Camera>().enabled = true;
            
            oldCam.GetComponent<SecurityCamera>().AudioListener.SetActive(false);
            newCam.GetComponent<SecurityCamera>().AudioListener.SetActive(true);
        }

        private void ToggleCams(bool isFocused)
        {
            PlayerCamera.GetComponent<Camera>().enabled = !isFocused;
            Cameras.Current.GetComponent<Camera>().enabled = isFocused;
            
            Cameras.Current.GetComponent<SecurityCamera>().AudioListener.SetActive(isFocused);

            var focus = PlayerFocusController.Instance;
            
            AudioListener.volume = isFocused ? focus.FocusedAudioVolume : focus.UnfocusedAudioVolume;

            if (isFocused)
            {
                FocusCamSound.Play();
            }
        }
    }
}