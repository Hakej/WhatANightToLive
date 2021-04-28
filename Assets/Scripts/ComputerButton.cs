using Controllers;
using Singletons;
using TMPro;
using UnityEngine;
using UI;

public class ComputerButton : MonoBehaviour
{
    public GameObject Feature;
    public TextMeshProUGUI Text;

    public Color ActiveColor;

    private ComputerController _computerController;
    private ComputerFeaturesController _computerFeaturesController;

    private Color _inactiveColor;

    private void Start()
    {
        _computerController = FindObjectOfType<ComputerController>();
        _computerFeaturesController = FindObjectOfType<ComputerFeaturesController>();

        _inactiveColor = Text.color;

        CheckColor(_computerFeaturesController.CurrentFeature);

        EventManager.Instance.OnComputerUIActiveFeatureChange += OnComputerUIActiveFeatureChange;
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
            _computerFeaturesController.ChangeFeature(Feature);
            ButtonsMenu.Instance.PlayConfirm();
        }
        else
        {
            _computerController.ToggleFocus(false);
        }
    }
}
