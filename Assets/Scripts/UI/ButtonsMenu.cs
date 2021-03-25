using UnityEngine;

namespace UI
{
    public class ButtonsMenu : Singleton<ButtonsMenu>
    {
        public AudioSource AudioSource;

        public AudioClip HoverSound;
        public AudioClip ConfirmSound;

        public void PlayHover()
        {
            AudioSource.PlayOneShot(HoverSound);
        }

        public void PlayConfirm()
        {
            AudioSource.PlayOneShot(ConfirmSound);
        }
    }

}