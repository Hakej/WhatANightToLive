using UnityEngine;

public class DebugHandler : Singleton<DebugHandler>
{
    public bool IsDebugModeOn = true;
    public Canvas DebugUICanvas;

    private void Start()
    {
        UpdateDebug();
    }

    public void ToggleDebugMode()
    {
        IsDebugModeOn = !IsDebugModeOn;
        UpdateDebug();
    }

    private void UpdateDebug()
    {
        DebugUICanvas.gameObject.SetActive(IsDebugModeOn);
    }
}
