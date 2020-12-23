using Controllers;
using TMPro;
using UnityEngine;

public class CameraUI : MonoBehaviour
{
    public TextMeshProUGUI CurrentCameraText;

    private void Update()
    {
        var currentCamera = SecurityCamerasController.Instance.Cameras.Current;
        CurrentCameraText.text = currentCamera.transform.parent.name;
    }
}
