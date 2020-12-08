using System.Collections;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public bool IsRotating = true;

    public float HalfAngle = 15f;
    public float TimeToPause = 2f;
    public float VarianceInPause = 0.5f;
    public float Speed = 0.1f;

    private Quaternion _startAngle;
    private Quaternion _endAngle;

    private bool _isRotatingToTheRight = true;
    
    private void Start()
    {
        if (!IsRotating)
        {
            Destroy(this);
        }

        var rot = transform.localRotation;
        
        _startAngle = Quaternion.Euler(rot.x, rot.y - HalfAngle, rot.z);
        _endAngle = Quaternion.Euler(rot.x, rot.y + HalfAngle, rot.z);
        
        StartCoroutine(CameraRotation());
    }

    private IEnumerator CameraRotation()
    {
        while (true)
        {
            var byAngles = Vector3.up * HalfAngle;
            byAngles *= _isRotatingToTheRight ? 1 : -1;
            
            var fromAngle = transform.rotation;
            var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);

            for (var t = 0f; t < 1; t += Time.deltaTime / Speed)
            {
                transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
                yield return null;
            }
            
            _isRotatingToTheRight = !_isRotatingToTheRight;
            transform.rotation = toAngle;

            var currentTimeToPause = TimeToPause + Random.Range(0f, VarianceInPause);
            
            yield return new WaitForSeconds(currentTimeToPause);
        }
    }
}
