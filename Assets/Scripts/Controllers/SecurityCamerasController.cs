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
            _cameras.Current.SetActive(false);
            _cameras.MoveNext.SetActive(true);
        }

        public void PreviousCamera()
        {
            _cameras.Current.SetActive(false);
            _cameras.MovePrevious.SetActive(true);
        }

        private void ToggleCams(bool isFocused)
        {
            PlayerCamera.SetActive(!isFocused);
            _cameras.Current.SetActive(isFocused);

            var focus = PlayerFocusController.Instance;
            AudioListener.volume = isFocused ? focus.FocusedAudioVolume : focus.UnfocusedAudioVolume;
        }
    }
}
