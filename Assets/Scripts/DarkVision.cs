using System;
using System.Collections;
using UnityEngine;
using EventHandler = Handlers.EventHandler;

public class DarkVision : MonoBehaviour
{
    public float Speed = 2f;
    public float Intensity = 10f;
    public float Frequency = 5f;
    public Light DarkVisionLight;

    private IEnumerator _darkVision;
    private bool _isDarkVisionOn = false;

    public void Start()
    {
        DarkVisionLight.intensity = 0f;

        EventHandler.Instance.OnPowerToggle += ToggleDarkVision;
    }

    private void ToggleDarkVision(bool isPowerOn, string gameObjectTag)
    {
        if (_isDarkVisionOn != isPowerOn)
        {
            return;
        }

        _isDarkVisionOn = !isPowerOn;

        if (_isDarkVisionOn == false)
        {
            DarkVisionLight.intensity = 0f;
        }
    }

    private void Update()
    {
        if (_isDarkVisionOn == false)
        {
            return;
        }

        if (DarkVisionLight.intensity <= Intensity)
        {
            DarkVisionLight.intensity += Time.deltaTime * Speed;
        }
        else
        {
            DarkVisionLight.intensity += Mathf.Sin(Time.realtimeSinceStartup * Frequency) / 360 * Frequency;
        }
    }
}
