using Classes.Extensions;
using UnityEngine;
using Handlers;

public class Door : MonoBehaviour
{
    public bool IsClosed = false;
    public Animator DoorAnimator;

    [Header("Open/Close Audio")]
    public AudioSource OpenCloseAudioSource;
    public AudioClip OpenDoorAudioClip;
    public AudioClip CloseDoorAudioClip;

    [Header("Failed Attack Audio")]
    public AudioSource FailedAttackAudioSource;
    public AudioClip[] FailedAttackAudioClips;

    private void Start()
    {
        RefreshAnimator();
    }

    public void RefreshAnimator()
    {
        DoorAnimator.SetBool("IsClosed", IsClosed);
    }

    public void Open()
    {
        IsClosed = false;

        OpenCloseAudioSource.PlayOneShot(OpenDoorAudioClip);

        RefreshAnimator();
    }

    public void Close()
    {
        IsClosed = true;

        OpenCloseAudioSource.PlayOneShot(CloseDoorAudioClip);

        RefreshAnimator();
    }

    public void MakeAttackNoise()
    {
        var randomAudioClip = FailedAttackAudioClips.GetRandomElement();

        FailedAttackAudioSource.PlayOneShot(randomAudioClip);
    }
}
