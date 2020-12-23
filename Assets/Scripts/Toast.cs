using TMPro;
using UnityEngine;

public class Toast : Singleton<Toast>
{
    public AudioSource ToastSound;
    public Canvas Canvas;
    public TextMeshProUGUI Text;
    
    public void Show(string message, float time)
    {
        ToastSound.Play();
        
        Text.text = message;
        var textObj = Instantiate(Text, Canvas.transform);
        Destroy(textObj.gameObject, time);
    }
}
