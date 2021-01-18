using System.Collections;
using Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public GameObject MinimapButton;
    public Camera MinimapCamera;
    public string SecurityCamTag;

    public GameObject MinimapButtonsParent;
    public SecurityCamerasController SecCamController;

    public void Start()
    {
        var cameras = GameObject.FindGameObjectsWithTag(SecurityCamTag);

        foreach (var cam in  cameras)
        {
            if (cam.activeSelf == false)
            {
                continue;
            }

            var roomName = cam.transform.parent.name;
            var camPos = RectTransformUtility.WorldToScreenPoint(MinimapCamera, cam.transform.position) * 1.75f;
            var buttonObject = Instantiate(MinimapButton, MinimapButtonsParent.transform);
            buttonObject.transform.localPosition = camPos;
            buttonObject.name = roomName + "_SecCamBut";

            var button = buttonObject.GetComponent<Button>();
            var text = button.GetComponentInChildren<TextMeshProUGUI>();

            text.text = roomName;

            button.onClick.AddListener(() => SecCamController.ChangeCam(cam));
        }
    }
}