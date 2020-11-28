using TMPro;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public float StartingSanity = 100f;
    public float BaseSanityDrop = 0.05f;
    
    public TextMeshProUGUI SanityText;

    public AudioSource WhiteNoise;
    public float WhiteNoiseStartingSanity = 50f;
    public float MaxWhiteNoiseVolume = 0.1f;
    
    private float _currentSanity;
    private float _currentSanityDrop;

    private void Start()
    {
        _currentSanity = StartingSanity;
        _currentSanityDrop = BaseSanityDrop;
    }

    private void Update()
    {
        if (_currentSanity > 0f)
        {
            _currentSanity -= _currentSanityDrop * Time.deltaTime;
        }
        
        SanityText.text = $"Sanity: {(int)_currentSanity}%";

        var noiseVolume = (WhiteNoiseStartingSanity - _currentSanity) / WhiteNoiseStartingSanity * MaxWhiteNoiseVolume;

        WhiteNoise.volume = noiseVolume > 0 ? noiseVolume : 0f;
    }
}
