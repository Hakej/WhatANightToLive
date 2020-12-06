using UnityEngine;

public class PoweredDevice : MonoBehaviour
{
    public GameObject[] GameObjectsToToggle;

    private void Start()
    {
        EventHandler.Instance.OnPowerToggle += OnToggle;
    }

    private void OnToggle(bool activity, string poweredTag)
    {
        if (!CompareTag(poweredTag))
        {
            return;
        }

        foreach (var component in GameObjectsToToggle)
        {
            component.gameObject.SetActive(activity);
        }
    }
}
