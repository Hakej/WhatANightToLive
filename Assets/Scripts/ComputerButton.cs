using Controllers;
using Handlers;
using TMPro;
using UnityEngine;
using UI;

public class ComputerButton : MonoBehaviour
{
    public GameObject Feature;
    public TextMeshProUGUI Text;

    public Color ActiveColor;
    private Color _inactiveColor;

    private void Start()
    {
        _inactiveColor = Text.color;

        CheckColor(ComputerFeaturesController.Instance.CurrentFeature);

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

    public void PointerIn()
    {
        Text.text = ">" + Text.text;
        ButtonsMenu.Instance.PlayHover();
    }

    public void PointerOut()
    {
        Text.text = Text.text.Replace(">", string.Empty);
    }

    public void PointerDown()
    {
        if (Feature != null)
        {
            ComputerFeaturesController.Instance.ChangeFeature(Feature);
            ButtonsMenu.Instance.PlayConfirm();
        }
        else
        {
            ComputerController.Instance.ToggleFocus(false);
        }
    }
}
