using Classes.Static;
using UnityEngine;
using UnityEngine.Rendering;

public class Sunrise : Singleton<Sunrise>
{
    public float SunriseLengthInGameMinutes;
    
    public GameObject Sun;
    public Sun SunScript;

    public AudioSource SunriseMusic;
    public Volume SunrisePostProcessVolume;

    private bool _isSunriseGoing = false;
    private float _currentSunriseTime = 0f;
    
    public void StartSunrise()
    {
        if (_isSunriseGoing)
        {
            return;
        }

        _isSunriseGoing = true;
        Sun.SetActive(true);
        SunriseMusic.Play();
    }

    public void Update()
    {
        if (!_isSunriseGoing)
        {
            return;
        }

        _currentSunriseTime += Time.deltaTime;

        var t = _currentSunriseTime / SunScript.PhaseTime;

        SunrisePostProcessVolume.weight = Mathf.Lerp(0, 1, t);
    }
}
