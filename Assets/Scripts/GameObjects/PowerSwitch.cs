using Handlers;
using UnityEngine;

public class PowerSwitch : MonoBehaviour
{
    public PowerHandler PowerHandler;
    public GameObject LightSwitch;
    public AudioSource LightSwitchSound;
    public Vector3 LightSwitchRotationOn;
    public Vector3 LightSwitchRotationOff;

    private void OnMouseDown()
    {
        ToggleLightSwitch();
    }

    private void ToggleLightSwitch()
    {
        PowerHandler.TogglePower();

        var newRotation = PowerHandler.IsPowerOn ? LightSwitchRotationOn : LightSwitchRotationOff;

        LightSwitch.transform.localRotation = Quaternion.Euler(newRotation);

        LightSwitchSound.Play();
    }
}