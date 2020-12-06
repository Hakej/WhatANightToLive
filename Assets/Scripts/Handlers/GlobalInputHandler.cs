using Handlers;
using UnityEngine;

public class GlobalInputHandler : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DebugHandler.Instance.ToggleDebugMode();
        }
    }
}
