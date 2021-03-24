using GameObjects;
using Handlers;
using UnityEngine;

public class AudioDecoy : MonoBehaviour
{
    public Room Room;

    [Header("Audio")]
    public AudioSource DecoyAudioSource;
    public AudioClip DecoyAudioClip;
    public AudioSource DecoyDestroyAudioSource;
    public AudioClip DecoyDestroyAudioClip;

    public SphereCollider AudioDecoyCollider;

    public bool IsPlaying { get => DecoyAudioSource.isPlaying; }

    [Header("Decoy destruction")]
    public bool IsDestroyed = false;
    public float MinDestructionCooldown = 15f;
    public float MaxDestructionCooldown = 60f;

    private float _currentDestructionCooldown = 0f;
    private float _destructionCooldown;


    private void Start()
    {
        AudioDecoyCollider.enabled = IsPlaying;
    }

    private void Update()
    {
        if (!IsDestroyed)
        {
            return;
        }

        _currentDestructionCooldown += Time.deltaTime;

        if (_currentDestructionCooldown >= _destructionCooldown)
        {
            FixDecoy();
        }
    }

    public void FixDecoy()
    {
        IsDestroyed = false;
        _currentDestructionCooldown = 0f;

        EventHandler.Instance.FixedAudioDecoy(this);
    }

    public void DestroyDecoy()
    {
        IsDestroyed = true;

        if (DecoyAudioSource.isPlaying)
        {
            DecoyAudioSource.Stop();
        }

        AudioDecoyCollider.enabled = false;
        DecoyDestroyAudioSource.PlayOneShot(DecoyDestroyAudioClip);

        _destructionCooldown = Random.Range(MinDestructionCooldown, MaxDestructionCooldown);

        EventHandler.Instance.DestroyedAudioDecoy(this);
    }

    public void ToggleDecoy()
    {
        if (IsDestroyed)
        {
            return;
        }

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
}
