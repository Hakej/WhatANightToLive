using Singletons;
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
            EventManager.Instance.OnSuccessfulAttack += OnSuccessfulAttack;
        }

        private void OnDisable()
        {
            if (EventManager.Instance != null)
            {
                EventManager.Instance.OnSuccessfulAttack -= OnSuccessfulAttack;
            }
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
