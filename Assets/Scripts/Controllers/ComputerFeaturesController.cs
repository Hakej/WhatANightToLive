using Singletons;
using UnityEngine;

public class ComputerFeaturesController : MonoBehaviour
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

        EventManager.Instance.ComputerUIActiveFeatureChange(newFeature);
    }
}
