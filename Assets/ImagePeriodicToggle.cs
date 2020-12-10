using UnityEngine;
using UnityEngine.UI;

public class ImagePeriodicToggle : MonoBehaviour
{
    public Image ImageToToggle;
    public float Interval;

    private float _currentTime;

    private bool _toggleState;

    private void Start()
    {
        _toggleState = ImageToToggle.enabled;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime < Interval)
        {
            return;
        }

        _currentTime = 0f;
        _toggleState = !_toggleState;
        ImageToToggle.enabled = _toggleState;
    }
}