using Handlers;
using UnityEngine;

public class PlayerUIHandler : MonoBehaviour
{
    public GameObject UI;
    public GameObject FlashlightButton;

    private void Start()
    {
        var eh = EventHandler.Instance;

        eh.OnPlayerRotationStart += OnPlayerRotationStart;
        eh.OnPlayerRotationStop += OnPlayerRotationStop;
        eh.OnSuccessfulAttack += OnSuccessfulAttack;
    }

    private void OnPlayerRotationStart()
    {
        ToggleUI(false);
    }

    private void OnPlayerRotationStop(Quaternion newAngle)
    {
        ToggleUI(true);

        if ((int)newAngle.eulerAngles.y != 270)
        {
            FlashlightButton?.SetActive(true);
        }
        else
        {
            FlashlightButton?.SetActive(false);
        }
    }

    private void OnSuccessfulAttack(bool isPlayerFacingEnemy)
    {
        if (FlashlightButton)
        {
            Destroy(FlashlightButton);
            FlashlightButton = null;
        }

        if (UI)
        {
            Destroy(UI);
            UI = null;
        }

        Destroy(this);
    }

    private void ToggleUI(bool state)
    {
        UI?.SetActive(state);
    }
}
