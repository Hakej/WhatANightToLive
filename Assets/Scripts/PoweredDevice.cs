using System.Collections;
using Handlers;
using UnityEngine;

public class PoweredDevice : MonoBehaviour
{
    public GameObject[] GameObjectsToToggle;

    public AudioSource PowerSound;
    public AudioClip PowerOn;
    public AudioClip PowerRunning;
    public AudioClip PowerOff;

    public bool IsMuted = true;
    
    private void Awake()
    {
        EventHandler.Instance.OnPowerToggle += OnToggle;
    }

    private void OnToggle(bool isRunning, string poweredTag)
    {
        if (!CompareTag(poweredTag))
        {
            return;
        }

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
        PowerSound.loop = false;
        
        if (isRunning)
        {
            PowerSound.clip = PowerOn;
            PowerSound.Play();
            StartCoroutine(PlayAfterPowerOn());
        }
        else
        {
            StopAllCoroutines();
            PowerSound.clip = PowerOff;
            PowerSound.Play();
        }
    }
    
    private IEnumerator PlayAfterPowerOn()
    {
        yield return new WaitForSeconds(PowerOn.length);

        PowerSound.clip = PowerRunning;
        PowerSound.loop = true;
        PowerSound.Play();
    }
}
