using Controllers;
using Handlers;
using UnityEngine;
using Singletons;

public class Computer : MonoBehaviour
{
    public ComputerController ComputerController;
    public PowerHandler PowerHandler;

    public AudioSource PowerOnWithoutPowerAudioSource;

    private void Start()
    {
        EventManager.Instance.OnSuccessfulAttack += OnSuccessfulAttack;
    }

    private void OnSuccessfulAttack(bool isPlayerFacingEnemy)
    {
        Destroy(this);
    }

    private void OnMouseDown()
    {
        if (PowerHandler.IsPowerOn)
        {
            ComputerController.ToggleFocus(true);
        }
        else
        {
            PowerOnWithoutPowerAudioSource.Play();
        }
    }
}
