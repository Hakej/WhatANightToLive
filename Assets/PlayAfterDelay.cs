using UnityEngine;

public class PlayAfterDelay : MonoBehaviour
{
    public AudioSource AudioSource;
    public float Delay;

    private void Start()
    {
        AudioSource.PlayDelayed(Delay);
    }
}
