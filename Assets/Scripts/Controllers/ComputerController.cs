using System.Collections;
using System.Collections.Generic;
using Handlers;
using UnityEngine;

namespace Controllers
{
    public class ComputerController : Singleton<ComputerController>
    {
        public GameObject PlayerCamera;
        public GameObject ComputerCamera;

        public AudioSource ComputerAudioSource;

        public List<GameObject> ComputerNoises;

        public AudioClip ComputerEnterSound;
        public AudioClip ComputerLeaveSound;

        [HideInInspector]
        public bool IsOnComputer = false;

        private void Start()
        {
            ComputerCamera.SetActive(IsOnComputer);
            PlayerCamera.SetActive(!IsOnComputer);
        }

        public void ToggleFocus(bool isOnComputer)
        {
            IsOnComputer = isOnComputer;

            ComputerCamera.SetActive(IsOnComputer);
            PlayerCamera.SetActive(!IsOnComputer);

            ComputerAudioSource.clip = IsOnComputer ? ComputerEnterSound : ComputerLeaveSound;
            ComputerAudioSource.Play();

            foreach (var computerNoise in ComputerNoises)
            {
                computerNoise.SetActive(IsOnComputer);
            }

            EventHandler.Instance.PlayerFocusChange(IsOnComputer);
        }
    }
}
