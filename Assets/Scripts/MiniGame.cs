using Controllers;
using Handlers;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    public float SanityGainOnFinish;
    
    public void FinishMiniGame()
    {
        GameHandler.Instance.Sanity.GainSanity(SanityGainOnFinish);
        MiniGameHandler.Instance.FinishMiniGame(gameObject);
    }
}
