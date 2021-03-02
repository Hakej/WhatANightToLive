using Handlers;
using UnityEngine;

public class PowerSwitch : MonoBehaviour
{
    public GameObject LightSwitch;
    public AudioSource LightSwitchSound;
    public Vector3 LightSwitchRotationOn;
    public Vector3 LightSwitchRotationOff;
    
    private bool _isLightSwitchOn;

    private void Start()
    {
        ToggleLightSwitch(GameHandler.Instance.IsPlayersPowerOn);
    }

    private void OnMouseDown()
    {
        ToggleLightSwitch(!_isLightSwitchOn);
    }

    private void ToggleLightSwitch(bool isLightSwitchOn)
    {
        _isLightSwitchOn = isLightSwitchOn;
        
        var newRotation = _isLightSwitchOn ? LightSwitchRotationOn : LightSwitchRotationOff;

        LightSwitch.transform.localRotation = Quaternion.Euler(newRotation);
        
        LightSwitchSound.Play();

        EventHandler.Instance.PowerToggle(_isLightSwitchOn, tag);
    }
}