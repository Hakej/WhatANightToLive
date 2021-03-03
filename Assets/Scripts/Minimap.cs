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

    public Color SelectedCameraButtonColor;
    
    private Color _unselectedCameraButtonColor;
    
    private Coroutine _currentSelectedCamCoroutine;
    private TextMeshProUGUI _currentButtonText;

    private Image _currentButtonImage;
    
    private void Start()
    {
        var cameras = GameObject.FindGameObjectsWithTag(SecurityCamTag);

        foreach (var cam in  cameras)
        {
            if (cam == null)
            {
                continue;
            }

            var roomName = cam.transform.parent.name;
            var camPos = RectTransformUtility.WorldToScreenPoint(MinimapCamera, cam.transform.position) * 1.75f;
            var buttonObject = Instantiate(MinimapButton, MinimapButtonsParent.transform);
            
            buttonObject.transform.localPosition = camPos;
            buttonObject.name = roomName + "_SecCamBut";
            
            var btnImg = buttonObject.GetComponent<Image>();
            var button = buttonObject.GetComponent<Button>();
            var text = button.GetComponentInChildren<TextMeshProUGUI>();
            
            text.text = roomName;

            if (cam == SecCamController.CurrentCamera)
            {
                btnImg.color = SelectedCameraButtonColor;
                _currentButtonImage = btnImg;
            }
            else
            {
                _unselectedCameraButtonColor = btnImg.color;
            }

            button.onClick.AddListener(() => ChangeCamAndButtonText(cam, btnImg));
        }
    }

    private void ChangeCamAndButtonText(GameObject cam, Image image)
    {
        if (_currentButtonImage != null)
        {
            _currentButtonImage.color = _unselectedCameraButtonColor;
        }
        
        SecCamController.ChangeCam(cam);
        
        image.color = SelectedCameraButtonColor;

        _currentButtonImage = image;
    }
}