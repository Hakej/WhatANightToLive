using UnityEngine;
using UnityEngine.UI;

public class AudioDecoyButton : MonoBehaviour
{
    public AudioDecoy AudioDecoy;
    public Image Image;
    public Sprite SpriteDecoyPlaying;
    public Sprite SpriteDecoyNotPlaying;

    private void Start()
    {
        CheckSprite();
    }

    public void ToggleAudioDecoy()
    {
        AudioDecoy.ToggleDecoy();
        CheckSprite();
    }

    public void CheckSprite()
    {
        if (AudioDecoy.IsPlaying)
        {
            Image.sprite = SpriteDecoyPlaying;
        }
        else
        {
            Image.sprite = SpriteDecoyNotPlaying;
        }
    }
}
