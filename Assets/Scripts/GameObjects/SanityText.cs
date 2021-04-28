using Handlers;
using TMPro;
using UnityEngine;

public class SanityText : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public float MinAlpha;
    public float MaxAlpha;

    public SanityHandler SanityHandler;

    private void Start()
    {
        InvokeRepeating("UpdateEverySecond", 0f, 1.0f);
    }

    private void UpdateEverySecond()
    {
        var currentSanity = SanityHandler.CurrentSanity;

        if (SanityHandler.CurrentSanity > 50f)
        {
            Text.color = Color.white;
        }
        else
        {
            Text.color = Color.Lerp(Color.red, Color.white, currentSanity / 50f);
        }
    }

    private void Update()
    {
        Text.alpha = Random.Range(MinAlpha, MaxAlpha);
    }
}
