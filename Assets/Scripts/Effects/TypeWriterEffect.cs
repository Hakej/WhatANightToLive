using System.Collections;
using TMPro;
using UnityEngine;

public class TypeWriterEffect : MonoBehaviour
{
    public float Delay = 0.1f;
    public float InitialDelay = 2f;

    public TextMeshProUGUI Text;
    
    private string _fullText;
    private string _currentText = "";

    private void Start()
    {
        _fullText = Text.text;
        Text.text = "";
        StartCoroutine(ShowText());
    }

    private IEnumerator ShowText()
    {
        yield return new WaitForSeconds(InitialDelay);

        for (var i = 0; i <= _fullText.Length; i++)
        {
            _currentText = _fullText.Substring(0, i);
            Text.text = _currentText;
            yield return new WaitForSeconds(Delay);
        }
    }
}
