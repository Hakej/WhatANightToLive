using TMPro;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public float MinOutline = 0.3f;
    public float MaxOutline = 0.4f;
    public float Speed = 1f;
    
    private float _step;

    private void Start()
    {
        _step = MaxOutline - MinOutline;
    }
    
    private void Update()
    {
        Text.outlineWidth = MinOutline + Mathf.Abs(Mathf.Sin(Speed * Time.realtimeSinceStartup)) * _step;
    }
}
