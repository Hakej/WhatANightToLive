using Handlers;
using UnityEngine;

public class FeaturesController : Singleton<FeaturesController>
{
    public GameObject CurrentFeature;

    public void ChangeFeature(GameObject newFeature)
    {
        CurrentFeature.SetActive(false);
        CurrentFeature = newFeature;
        CurrentFeature.SetActive(true);

        EventHandler.Instance.ComputerUIActiveFeatureChange(newFeature);
    }
}
