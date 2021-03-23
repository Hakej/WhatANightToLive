using Handlers;
using TMPro;
using UnityEngine;

public class ComputerButton : MonoBehaviour
{
    public GameObject Feature;
    public TextMeshProUGUI Text;

    public Color ActiveColor;
    private Color _inactiveColor;

    private void Start()
    {
        _inactiveColor = Text.color;

        CheckColor(FeaturesController.Instance.CurrentFeature);

        EventHandler.Instance.OnComputerUIActiveFeatureChange += OnComputerUIActiveFeatureChange;
    }

    private void CheckColor(GameObject feature)
    {
        if (feature == Feature)
        {
            Text.color = ActiveColor;
        }
        else
        {
            Text.color = _inactiveColor;
        }
    }

    private void OnComputerUIActiveFeatureChange(GameObject feature)
    {
        CheckColor(feature);
    }

    private void OnEnable()
    {
        Text.text = Text.text.Replace(">", string.Empty);
    }

    public void HoverIn()
    {
        Text.text = ">" + Text.text;
    }

    public void HoverOut()
    {
        Text.text = Text.text.Replace(">", string.Empty);
    }

    public void ChangeFeature()
    {
        FeaturesController.Instance.ChangeFeature(Feature);
    }
}
