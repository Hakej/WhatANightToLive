using System.Collections;
using System.Collections.Generic;
using Handlers;
using UnityEngine;

namespace Controllers
{
    public class ComputerController : Singleton<ComputerController>
    {

        public AudioSource ComputerAudioSource;

        public List<GameObject> ComputerNoises;

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

            foreach (var computerNoise in ComputerNoises)
            {
                computerNoise.SetActive(isOnComputer);
            }

            EventHandler.Instance.PlayerFocusChange(IsOnLaptop);
        }
    }
}
