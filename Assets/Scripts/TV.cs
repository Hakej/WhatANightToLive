using TMPro;
using UnityEngine;

public class TV : MonoBehaviour
{
    public AudioSource WhiteNoise;
    public TextMeshProUGUI SanityText;
    
    public float MaxWhiteNoiseVolume = 0.1f;
    public float WhiteNoiseStartingSanity = 50f;

    private void Update()
    {
        var curSanity = GameHandler.CurrentSanity;
        
        SanityText.text = $"Sanity: {(int)curSanity}%";
        
        var noiseVolume = (WhiteNoiseStartingSanity - curSanity) / WhiteNoiseStartingSanity * MaxWhiteNoiseVolume;
        WhiteNoise.volume = noiseVolume > 0 ? noiseVolume : 0f;
    }
}
