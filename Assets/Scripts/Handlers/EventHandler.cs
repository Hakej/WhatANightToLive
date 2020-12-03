using System;

public class EventHandler : Singleton<EventHandler>
{
    public event Action OnWin;
    public event Action OnLose;
    public event Action<bool> OnPlayerFocusChangeStart;
    public event Action<bool> OnPlayerFocusChangeStop;

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
}
