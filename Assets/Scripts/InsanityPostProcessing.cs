using Handlers;
using UnityEngine;
using UnityEngine.Rendering;

public class InsanityPostProcessing : MonoBehaviour
{
    public float SanityThreshold = 50f;
    public Volume Volume;

    public float MinimumWeight = 0f;
    public float MaximumWeight = 0.2f;

    public SanityHandler SanityHandler;

    private void Update()
    {
        var currentSanity = SanityHandler.CurrentSanity;

        if (currentSanity >= SanityThreshold)
        {
            Volume.weight = 0f;
            return;
        }

        Volume.weight = Mathf.Lerp(MaximumWeight, MinimumWeight, currentSanity / 50f);
    }
}
