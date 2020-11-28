using System;
using System.Collections;
using UnityEngine;

public class Laptop : MonoBehaviour
{
    public Camera Camera;
    public GameObject UICanvas;
    public float FocusedFOV = 12f;
    public float UnfocusedFOV = 60f;
    public float FocusingSpeed = 1f;
    public float FocusedCameraRotationX = 3f;
    
    private bool _isFocused = false;
    private bool _isFocusing = false;
    
    private void OnMouseDown()
    {
        if (_isFocusing) return;

        StartCoroutine(Focus());
    }
    
    private IEnumerator Focus()
    {
        _isFocusing = true;
        UICanvas.SetActive(_isFocused);
        
        var fromFOV = Camera.fieldOfView;
        var toFOV = _isFocused ? UnfocusedFOV : FocusedFOV;

        var fromX = Camera.transform.rotation.x;
        var toX = _isFocused ? 0f : FocusedCameraRotationX;
        
        for (var t = 0f; t < 1; t += Time.deltaTime / FocusingSpeed)
        {
            var lerpedX = Mathf.Lerp(fromX, toX, t);
            
            Camera.fieldOfView = Mathf.Lerp(fromFOV, toFOV, t);
            Camera.transform.localRotation = Quaternion.AngleAxis(lerpedX, Vector3.right);
            
            yield return null;
        }

        Camera.fieldOfView = toFOV;
        Camera.transform.localRotation = Quaternion.Euler(toX, 0, 0);
        
        _isFocused = !_isFocused;
        _isFocusing = false;
    }
}
