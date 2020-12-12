using System.Collections;
using Handlers;
using UnityEngine;

namespace Controllers
{
    public class PlayerFocusController : Singleton<PlayerFocusController>
    {
        public Camera PlayerCamera;
    
        [Header("Focus")]
        public float FocusedFOV = 12f;
        public float UnfocusedFOV = 60f;
        public float FocusingSpeed = 0.25f;
        public float FocusedCameraRotationX = 3f;
    
        public float FocusedAudioVolume = 0.5f;
        public float UnfocusedAudioVolume = 1f;

        [HideInInspector]
        public bool IsFocused = false;

        private bool _isFocusChanging = false;

        private void Start()
        {
            UIHandler.Instance.ToggleCameraUI(IsFocused);
            UIHandler.Instance.TogglePlayerUI(!IsFocused);
        }

        public void ToggleFocus()
        {
            if (_isFocusChanging) return;
            StartCoroutine(ChangeFocus());
        }
    
        private IEnumerator ChangeFocus()
        {
            _isFocusChanging = true;

            EventHandler.Instance.PlayerFocusChangeStart(IsFocused);
        
            var fromFOV = PlayerCamera.fieldOfView;
            var fromX = PlayerCamera.transform.rotation.x;
        
            var toFOV = IsFocused ? UnfocusedFOV : FocusedFOV;
            var toX = IsFocused ? 0f : FocusedCameraRotationX;
        
            for (var t = 0f; t < 1; t += Time.deltaTime / FocusingSpeed)
            {
                var lerpedX = Mathf.Lerp(fromX, toX, t);
            
                PlayerCamera.fieldOfView = Mathf.Lerp(fromFOV, toFOV, t);
                PlayerCamera.transform.localRotation = Quaternion.AngleAxis(lerpedX, Vector3.right);
            
                yield return null;
            }

            PlayerCamera.fieldOfView = toFOV;
            PlayerCamera.transform.localRotation = Quaternion.Euler(toX, 0, 0);
        
            IsFocused = !IsFocused;
        
            UIHandler.Instance.ToggleCameraUI(IsFocused);
            UIHandler.Instance.TogglePlayerUI(!IsFocused);

            EventHandler.Instance.PlayerFocusChangeStop(IsFocused);
        
            _isFocusChanging = false;
        }
    }
}
