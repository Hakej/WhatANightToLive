using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorTintFix : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
    }

    public void FixButton()
    {
        _button.enabled = false;
        _button.enabled = true;
    }
}
