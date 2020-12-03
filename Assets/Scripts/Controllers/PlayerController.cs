using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float RotationSpeed = 0.5f;

    public GameObject UICanvas;
    public GameObject Flashlight;

    private bool _isTurning;

    public void StartUsingFlashlight()
    {
        Flashlight.SetActive(true);
    }

    public void StopUsingFlashlight()
    {
        Flashlight.SetActive(false);
    }
    
    public void TurnLeft()
    {
        if (_isTurning) return;
        StartCoroutine(RotateMe(Vector3.up * -90));
    }

    public void TurnRight()
    {
        if (_isTurning) return;
        StartCoroutine(RotateMe(Vector3.up * 90));
    }

    public void TurnAround()
    {
        if (_isTurning) return;
        StartCoroutine(RotateMe(Vector3.up * 180));
    }
    
    private IEnumerator RotateMe(Vector3 byAngles)
    {
        _isTurning = true;
        UICanvas.SetActive(false);
        
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        
        for (var t = 0f; t < 1; t += Time.deltaTime / RotationSpeed)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }

        transform.rotation = toAngle;
        UICanvas.SetActive(true);
        _isTurning = false;
    }
}