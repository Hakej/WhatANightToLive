using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float MouseSensitivity = 100f;
    
    private float _xRotation = 0f;
    private Transform _playerTransform;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        var mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        _playerTransform.Rotate(Vector3.up * mouseX);
    }
}