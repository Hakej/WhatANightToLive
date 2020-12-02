using System;

public class EventHandler : Singleton<EventHandler>
{
    public event Action OnWin;
    public event Action OnLose;

    public void Win()
    {
        OnWin?.Invoke();
    }

    public void Lose()
    {
        OnLose?.Invoke();
    }
}
