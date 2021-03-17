using System.Collections;
using Handlers;
using UnityEngine;

namespace Controllers
{
    public class ComputerController : Singleton<ComputerController>
    {

        public AudioSource ComputerAudioSource;
        public AudioSource ComputerNoiseAudioSource;

        public AudioClip ComputerEnterSound;
        public AudioClip ComputerLeaveSound;

        public float OnComputerAudioVolume = 0.5f;
        public float OffComputerAudioVolume = 1f;

        [HideInInspector]
        public bool IsOnLaptop = false;

        private void Start()
        {
            UIHandler.Instance.ToggleCameraUI(IsOnLaptop);
            UIHandler.Instance.TogglePlayerUI(!IsOnLaptop);
        }

        public void ToggleFocus(bool isOnComputer)
        {
            IsOnLaptop = isOnComputer;

            UIHandler.Instance.ToggleCameraUI(IsOnLaptop);
            UIHandler.Instance.TogglePlayerUI(!IsOnLaptop);

            AudioListener.volume = IsOnLaptop ? OnComputerAudioVolume : OffComputerAudioVolume;

            ComputerAudioSource.clip = isOnComputer ? ComputerEnterSound : ComputerLeaveSound;

            ComputerAudioSource.Play();

            if (ComputerNoiseAudioSource.isPlaying)
            {
                ComputerNoiseAudioSource.Stop();
            }
            else
            {
                ComputerNoiseAudioSource.PlayDelayed(0.5f);
            }

            EventHandler.Instance.PlayerFocusChange(IsOnLaptop);
        }
    }
}
