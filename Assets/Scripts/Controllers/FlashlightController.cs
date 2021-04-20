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

        public void ToggleFlashlight(bool state)
        {
            FlashLight.SetActive(state);

            var sound = state ? FlashlightOnSound : FlashlightOffSound;
            AudioSource.PlayOneShot(sound);
        }
    }
}
