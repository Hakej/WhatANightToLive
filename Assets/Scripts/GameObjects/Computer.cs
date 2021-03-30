using Controllers;
using Handlers;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public ComputerController ComputerController;

    public AudioSource PowerOnWithoutPowerAudioSource;

    private void OnMouseDown()
    {
        if (GameHandler.Instance.IsPowerOn)
        {
            ComputerController.ToggleFocus(true);
        }
        else
        {
            PowerOnWithoutPowerAudioSource.Play();
        }
    }
}
