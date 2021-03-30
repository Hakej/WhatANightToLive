using System.Collections.Generic;
using Classes.Extensions;
using UnityEngine;

public class AmbientSoundsHandler : MonoBehaviour
{

    [Header("Cooldown")]
    public float MinCooldown;
    public float MaxCooldown;

    private float _currentCooldown;
    private AudioSource[] _redHerringAudioSources;

    private void Start()
    {
        _currentCooldown = Random.Range(MinCooldown, MaxCooldown);

        _redHerringAudioSources = GetComponentsInChildren<AudioSource>();
    }

    private void Update()
    {
        _currentCooldown -= Time.deltaTime;

        if (_currentCooldown > 0f)
        {
            return;
        }

        var randomRedHerring = _redHerringAudioSources.GetRandomElement();

        randomRedHerring.Play();

        _currentCooldown = Random.Range(MinCooldown, MaxCooldown);
    }
}
