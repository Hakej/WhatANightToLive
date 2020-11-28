using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float RotationSpeed = 0.5f;

    private bool _isTurning = false;

    private void Update()
    {
        if (_isTurning)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(RotateMe(Vector3.up * -90));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(RotateMe(Vector3.up * 90));
        }
    }

    private IEnumerator RotateMe(Vector3 byAngles)
    {
        _isTurning = true;
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / RotationSpeed)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }

        transform.rotation = toAngle;
        _isTurning = false;
    }
}