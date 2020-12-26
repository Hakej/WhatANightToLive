using Handlers;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public TextMeshPro TimeText;
    
    private void Update()
    {
        TimeText.text = GameHandler.Instance.GameTime.CurrentTime.ToString();
    }
}
