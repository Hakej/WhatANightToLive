using Classes.Static;
using TMPro;
using UnityEngine;

public class SanityText : MonoBehaviour
{
    public TextMeshProUGUI Text;
    private void Start()
    {
        InvokeRepeating("UpdateEverySecond", 0f, 1.0f);
    }

    private void UpdateEverySecond()
    {
        var currentSanity = SanityHandler.Instance.CurrentSanity;

        if (SanityHandler.Instance.CurrentSanity > 50f)
        {
            Text.color = Color.white;
        }
        else
        {
            Text.color = Color.Lerp(Color.red, Color.white, currentSanity / 50f);
        }
    }
}
