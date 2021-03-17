using System.Collections;
using Handlers;
using UnityEngine;

namespace Controllers
{
    public class ComputerController : Singleton<ComputerController>
    {
        public AudioSource LaptopEnterSound;
        public float OnLaptopAudioVolume = 0.5f;
        public float OffLaptopAudioVolume = 1f;

        [HideInInspector]
        public bool IsOnLaptop = false;

        private void Start()
        {
            UIHandler.Instance.ToggleCameraUI(IsOnLaptop);
            UIHandler.Instance.TogglePlayerUI(!IsOnLaptop);
        }

        public void ToggleFocus(bool isOnLaptop)
        {
            IsOnLaptop = isOnLaptop;

            UIHandler.Instance.ToggleCameraUI(IsOnLaptop);
            UIHandler.Instance.TogglePlayerUI(!IsOnLaptop);

            AudioListener.volume = IsOnLaptop ? OnLaptopAudioVolume : OffLaptopAudioVolume;

            if (IsOnLaptop)
            {
                LaptopEnterSound.Play();
            }

            EventHandler.Instance.PlayerFocusChange(IsOnLaptop);
        }
    }
}
