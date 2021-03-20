using TMPro;
using UnityEngine;

public class ComputerButton : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void OnEnable()
    {
        text.text = text.text.Replace(">", string.Empty);

    }

    public void HoverIn()
    {
        text.text = ">" + text.text;
    }

    public void HoverOut()
    {
        text.text = text.text.Replace(">", string.Empty);
    }
}
