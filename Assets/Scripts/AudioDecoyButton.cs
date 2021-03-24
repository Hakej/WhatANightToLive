using Handlers;
using UnityEngine;
using UnityEngine.UI;

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

        EventHandler.Instance.OnDestroyedAudioDecoy += OnDestroyedAudioDecoy;
        EventHandler.Instance.OnFixedAudioDecoy += OnFixedAudioDecoy;
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

    public void ToggleAudioDecoy()
    {
        if (AudioDecoy.IsDestroyed)
        {
            return;
        }

        AudioDecoy.ToggleDecoy();
        CheckSprite();
    }

    public void CheckSprite()
    {
        Animator.enabled = AudioDecoy.IsPlaying;
        Image.sprite = AudioDecoy.IsPlaying ? SpriteDecoyPlaying : SpriteDecoyNotPlaying;
    }
}
