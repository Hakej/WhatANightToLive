using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static float CurrentSanity;

    public float StartingSanity = 100f;
    public float BaseSanityDrop = 0.05f;
    
    private float _currentSanityDrop;

    private void Start()
    {
        CurrentSanity = StartingSanity;
        _currentSanityDrop = BaseSanityDrop;
    }

    private void Update()
    {
        if (CurrentSanity > 0f)
        {
            CurrentSanity -= _currentSanityDrop * Time.deltaTime;
        }
    }
}
