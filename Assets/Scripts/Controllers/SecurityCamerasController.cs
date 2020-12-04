using Classes;
using UnityEngine;

namespace Controllers
{
    public class SecurityCamerasController : Singleton<SecurityCamerasController>
    {
        public GameObject PlayerCamera;
    
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
            _cameras.Current.GetComponent<Camera>().enabled = false;
            _cameras.MoveNext.GetComponent<Camera>().enabled = true;
        }

        public void PreviousCamera()
        {
            _cameras.Current.GetComponent<Camera>().enabled = false;
            _cameras.MovePrevious.GetComponent<Camera>().enabled = true;
        }

        private void ToggleCams(bool isFocused)
        {
            PlayerCamera.GetComponent<Camera>().enabled = !isFocused;
            _cameras.Current.GetComponent<Camera>().enabled = isFocused;

            var focus = PlayerFocusController.Instance;
            AudioListener.volume = isFocused ? focus.FocusedAudioVolume : focus.UnfocusedAudioVolume;
        }
    }
}
