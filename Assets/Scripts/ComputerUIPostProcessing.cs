using System.Collections;
using Handlers;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ComputerUIPostProcessing : MonoBehaviour
{
    public float Delay;
    public float LoweringTime;
    public Volume Volume;

    private void Start()
    {
        EventHandler.Instance.OnComputerUIActiveFeatureChange += OnComputerUIActiveFeatureChange;
    }

    private void OnComputerUIActiveFeatureChange(GameObject feature)
    {
        // TODO: Add some visual effect for changing current feature
        //LowerChromaticAberration();
    }

    private void OnEnable()
    {
        Invoke("LowerChromaticAberration", Delay);
    }

    private void LowerChromaticAberration()
    {
        if (Volume.profile.TryGet<ChromaticAberration>(out var chromaticAberration))
        {
            StartCoroutine(LowerOverTime(chromaticAberration));
        }
    }

    private IEnumerator LowerOverTime(ChromaticAberration chromaticAberration)
    {
        var totalTime = 0f;
        chromaticAberration.intensity.value = 1f;

        while (totalTime < LoweringTime)
        {
            chromaticAberration.intensity.value = Mathf.Lerp(1f, 0f, totalTime / LoweringTime);
            totalTime += Time.deltaTime;
            yield return null;
        }
    }
}
