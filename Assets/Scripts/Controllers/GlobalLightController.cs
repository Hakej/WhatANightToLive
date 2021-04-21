using UnityEngine;

public class GlobalLightController : MonoBehaviour
{
    public GameObject GlobalLight;

    public KeyCode ToggleLightKey;

    private void Update()
    {
        if (Input.GetKeyDown(ToggleLightKey))
        {
            GlobalLight.SetActive(!GlobalLight.activeSelf);
        }
    }
}
