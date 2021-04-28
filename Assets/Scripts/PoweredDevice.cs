using System.Collections;
using Singletons;
using UnityEngine;

public class PoweredDevice : MonoBehaviour
{
    public GameObject[] GameObjectsToToggle;

    public AudioSource PowerAudioSource;
    public AudioClip PowerOn;
    public AudioClip PowerRunning;
    public AudioClip PowerOff;

    public bool IsMuted = true;

    private void Awake()
    {
        EventManager.Instance.OnPowerToggle += OnToggle;
    }

    private void OnToggle(bool isRunning)
    {
        if (!IsMuted)
        {
            PlayAudio(isRunning);
        }

        foreach (var component in GameObjectsToToggle)
        {
            component.gameObject.SetActive(isRunning);
        }
    }

    private void PlayAudio(bool isRunning)
    {
        PowerAudioSource.loop = false;

        if (isRunning)
        {
            PowerAudioSource.clip = PowerOn;
            PowerAudioSource.Play();
            StartCoroutine(PlayAfterPowerOn());
        }
        else
        {
            StopAllCoroutines();
            PowerAudioSource.clip = PowerOff;
            PowerAudioSource.Play();
        }
    }

    private IEnumerator PlayAfterPowerOn()
    {
        yield return new WaitForSeconds(PowerOn.length);

        PowerAudioSource.clip = PowerRunning;
        PowerAudioSource.loop = true;
        PowerAudioSource.Play();
    }
}
