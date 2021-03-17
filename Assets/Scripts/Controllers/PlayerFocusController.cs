using System.Collections;
using Handlers;
using UnityEngine;

namespace Controllers
{
    public class PlayerFocusController : Singleton<PlayerFocusController>
    {
        public float FocusedAudioVolume = 0.5f;
        public float UnfocusedAudioVolume = 1f;


        [HideInInspector]
        public bool IsFocused = false;

        private void Start()
        {
            UIHandler.Instance.ToggleCameraUI(IsFocused);
            UIHandler.Instance.TogglePlayerUI(!IsFocused);
        }

        public void ToggleFocus()
        {
            ChangeFocus();
        }

        private void ChangeFocus()
        {
            IsFocused = !IsFocused;

            UIHandler.Instance.ToggleCameraUI(IsFocused);
            UIHandler.Instance.TogglePlayerUI(!IsFocused);

            EventHandler.Instance.PlayerFocusChange(IsFocused);
        }
    }
}
