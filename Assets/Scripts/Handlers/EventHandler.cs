using System;
using UnityEngine;

public class EventHandler : Singleton<EventHandler>
{
    public event Action OnWin;
    public event Action OnLose;
    public event Action<bool> OnPlayerFocusChangeStart;
    public event Action<bool> OnPlayerFocusChangeStop;
    public event Action<bool, string> OnPowerToggle;

    public void Win()
    {
        OnWin?.Invoke();
    }

    public void Lose()
    {
        OnLose?.Invoke();
    }

    public void PlayerFocusChangeStart(bool isFocused)
    {
        OnPlayerFocusChangeStart?.Invoke(isFocused);
    }
    
    public void PlayerFocusChangeStop(bool isFocused)
    {
        OnPlayerFocusChangeStop?.Invoke(isFocused);
    }

    public void PlayersLightsToggle(bool isPowerOn, string gameObjectTag)
    {
        OnPowerToggle?.Invoke(isPowerOn, gameObjectTag);
    }
}
