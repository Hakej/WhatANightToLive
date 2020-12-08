using Classes;
using UnityEngine;

namespace Controllers
{
    public class SecurityCamerasController : Singleton<SecurityCamerasController>
    {
        public GameObject PlayerCamera;
        public AudioSource FocusCamSound;
    
        private LoopingList<GameObject> _cameras;

        private void Start()
        {
            _cameras = new LoopingList<GameObject>();
        
            foreach (Transform child in transform)
            {
                _cameras.Add(child.gameObject);
            }
        
            EventHandler.Instance.OnPlayerFocusChangeStop += ToggleCams;
        }

        public void NextCamera()
        {
            ChangeCam(_cameras.Current, _cameras.MoveNext);
        }

        public void PreviousCamera()
        {
            ChangeCam(_cameras.Current, _cameras.MovePrevious);
        }

        private void ChangeCam(GameObject oldCam, GameObject newCam)
        {
            //oldCam.SetActive(false);
            //newCam.SetActive(true);
            oldCam.GetComponent<Camera>().enabled = false;
            newCam.GetComponent<Camera>().enabled = true;
        }
        
        private void ToggleCams(bool isFocused)
        {
            //PlayerCamera.SetActive(!isFocused);
            //_cameras.Current.SetActive(isFocused);
            PlayerCamera.GetComponent<Camera>().enabled = !isFocused;
            _cameras.Current.GetComponent<Camera>().enabled = isFocused;
            
            var focus = PlayerFocusController.Instance;
            AudioListener.volume = isFocused ? focus.FocusedAudioVolume : focus.UnfocusedAudioVolume;

            if (isFocused)
            {
                FocusCamSound.Play();
            }
        }
    }
}
