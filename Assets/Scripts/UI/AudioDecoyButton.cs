using Singletons;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AudioDecoyButton : MonoBehaviour
    {
        public AudioDecoy AudioDecoy;
        public Image Image;
        public Sprite SpriteDecoyPlaying;
        public Sprite SpriteDecoyNotPlaying;
        public Sprite SpriteDecoyDestroyed;
        public Animator Animator;

        private void Start()
        {
            CheckSprite();

            EventManager.Instance.OnDestroyedAudioDecoy += OnDestroyedAudioDecoy;
            EventManager.Instance.OnFixedAudioDecoy += OnFixedAudioDecoy;
            EventManager.Instance.OnChangedAudioDecoy += OnChangedAudioDecoy;
        }

        private void OnChangedAudioDecoy(AudioDecoy oldDecoy, AudioDecoy newDecoy)
        {
            if (AudioDecoy == oldDecoy || AudioDecoy == newDecoy)
            {
                CheckSprite();
            }
        }

        private void OnDestroyedAudioDecoy(AudioDecoy audioDecoy)
        {
            if (audioDecoy == AudioDecoy)
            {
                Image.sprite = SpriteDecoyDestroyed;
                Image.color = Color.red;

                Animator.enabled = false;
            }
        }

        private void OnFixedAudioDecoy(AudioDecoy audioDecoy)
        {
            if (audioDecoy == AudioDecoy)
            {
                Image.sprite = SpriteDecoyNotPlaying;
                Image.color = Color.yellow;
            }
        }

        public void CheckSprite()
        {
            Animator.enabled = AudioDecoy.IsPlaying;
            Image.sprite = AudioDecoy.IsPlaying ? SpriteDecoyPlaying : SpriteDecoyNotPlaying;
        }
    }
}