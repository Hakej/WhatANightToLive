using Handlers;
using UnityEngine;
using UnityEngine.Rendering;

public class InsanityPostProcessing : MonoBehaviour
{
    public float SanityThreshold = 50f;
    public Volume Volume;

    private void Update()
    {
        var currentSanity = SanityHandler.Instance.CurrentSanity;

        if (currentSanity >= SanityThreshold)
        {
            Volume.weight = 0f;
            return;
        }

        Volume.weight = Mathf.Lerp(0.5f, 0f, currentSanity / 50f);
    }
}
