using UnityEngine;

public class AudioDecoy : MonoBehaviour
{
    public AudioSource DecoyAudioSource;

    public void ToggleDecoy()
    {
        if (DecoyAudioSource.isPlaying)
        {
            DecoyAudioSource.Stop();
        }
        else
        {
            DecoyAudioSource.Play();
        }
    }

    public void PlayAudioDecoy()
    {
        if (DecoyAudioSource.isPlaying)
        {
            return;
        }

        DecoyAudioSource.Play();
    }

    public void StopAudioDecoy()
    {
        if (!DecoyAudioSource.isPlaying)
        {
            return;
        }

        DecoyAudioSource.Stop();
    }
}
