using Handlers;
using UnityEngine;

public class ComputerFeaturesController : Singleton<ComputerFeaturesController>
{
    public GameObject CurrentFeature;
    public GameObject[] AllFeatures;

    private void Start()
    {
        foreach (var feature in AllFeatures)
        {
            feature.SetActive(feature == CurrentFeature);
        }
    }

    public void ChangeFeature(GameObject newFeature)
    {
        CurrentFeature.SetActive(false);
        CurrentFeature = newFeature;
        CurrentFeature.SetActive(true);

        EventHandler.Instance.ComputerUIActiveFeatureChange(newFeature);
    }
}
