using Controllers;
using Handlers;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    public float SanityGainOnFinish;
    
    public void FinishMiniGame()
    {
        GameHandler.Instance.SanityHandler.GainSanity(SanityGainOnFinish);
        MiniGameHandler.Instance.FinishMiniGame(gameObject);
    }
}
