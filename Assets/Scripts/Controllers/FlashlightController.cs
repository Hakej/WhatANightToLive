using UnityEngine;

namespace Controllers
{
    public class FlashlightController : MonoBehaviour
    {
        public AudioSource AudioSource;
        
        public AudioClip FlashlightOnSound;
        public AudioClip FlashlightOffSound;

        public Light FlashLight;

        public void ToggleFlashlight(bool state)
        {
            FlashLight.enabled = state;

            AudioSource.clip = state ? FlashlightOnSound : FlashlightOffSound;
            AudioSource.Play();
        }
    }
}
