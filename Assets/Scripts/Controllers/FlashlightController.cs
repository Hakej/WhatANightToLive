using Handlers;
using UnityEngine;

namespace Controllers
{
    public class FlashlightController : MonoBehaviour
    {
        public AudioSource AudioSource;

        public AudioClip FlashlightOnSound;
        public AudioClip FlashlightOffSound;
        public GameObject FlashLight;

        private void Start()
        {
            EventHandler.Instance.OnSuccessfulAttack += OnSuccessfulAttack;
        }

        private void OnSuccessfulAttack(bool isPlayerFacingEnemy)
        {
            if (FlashLight.activeSelf && !isPlayerFacingEnemy)
            {
                ToggleFlashlight(false);
            }
        }

        public void ToggleFlashlight(bool state)
        {
            FlashLight.SetActive(state);

            var sound = state ? FlashlightOnSound : FlashlightOffSound;
            AudioSource.PlayOneShot(sound);
        }
    }
}
