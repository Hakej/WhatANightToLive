using Handlers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TV : MonoBehaviour
{
    public TextMeshProUGUI SanityText;

    [Header("White Noise Hider")]
    public Image WhiteNoiseHider;
    public float MaxAlpha;
    public float MinAlpha;


    [Header("White Noise Sound")]
    public AudioSource WhiteNoiseSource;
    public float MaxWhiteNoiseVolume = 0.1f;
    public float WhiteNoiseStartingSanity = 50f;

    private void Update()
    {
        var curSanity = SanityHandler.Instance.CurrentSanity;

        SanityText.text = $"Sanity: {(int)curSanity}%";

        var noiseColor = WhiteNoiseHider.color;
        var noiseValue = (WhiteNoiseStartingSanity - curSanity) / WhiteNoiseStartingSanity;

        var noiseVolume = noiseValue > 0 ? noiseValue * MaxWhiteNoiseVolume : 0f;
        var noiseAlpha = noiseValue > 0 ? MaxAlpha - noiseValue : MaxAlpha;

        noiseColor.a = noiseAlpha;

        WhiteNoiseHider.color = noiseColor;
        WhiteNoiseSource.volume = noiseVolume;
    }
}
