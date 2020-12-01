using System;

public class EventHandler : Singleton<EventHandler>
{
    public event Action OnWin;

    public void Win()
    {
        if (OnWin != null)
        {
            OnWin();
        }
    }
}
