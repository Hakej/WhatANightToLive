using GameObjects;
using UnityEngine;

public class AudioDecoy : MonoBehaviour
{
    public Room Room;
    public AudioSource DecoyAudioSource;

    [Header("Collider")]
    public SphereCollider AudioDecoyCollider;

    public bool IsPlaying { get => DecoyAudioSource.isPlaying; }

    private void Start()
    {
        AudioDecoyCollider.enabled = IsPlaying;
    }

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

        AudioDecoyCollider.enabled = IsPlaying;
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
