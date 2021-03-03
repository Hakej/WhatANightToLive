using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightFlicker : MonoBehaviour
{
    public Light Light;
    public float LowIntensity;
    
    private float _baseIntensity;

    private void Start()
    {
        _baseIntensity = Light.intensity;
    }

    private void Update()
    {
        Light.intensity = Random.Range(LowIntensity, _baseIntensity);
    }

    private void OnDisable()
    {
        Light.intensity = _baseIntensity;
    }
}
