using System;
using UnityEngine;

public class PeriodicalTextScale : MonoBehaviour
{
    public float Variance = 0.1f;
    public float Speed = 5f;
    public RectTransform RectTransform;
    
    private Vector3 _startingScale;
    
    private void Update()
    {
        var variance = 1 + (float)Math.Sin(Time.timeSinceLevelLoad * Speed) * Variance;
        RectTransform.localScale = Vector3.one * variance;
    }
}
