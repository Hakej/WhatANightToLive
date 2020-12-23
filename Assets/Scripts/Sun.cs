using UnityEngine;

public class Sun : MonoBehaviour
{
    public float PhaseTime = 60f;
    
    private float _currentPhaseTime = 0f;
        
    private readonly Vector3 _startPosition = new Vector3(-30f, -90f, 0f);
    private readonly Vector3 _risePosition = new Vector3(0f, -90f, 0f);
    
    private void Update()
    {
        _currentPhaseTime += Time.deltaTime;
        
        var t = _currentPhaseTime / PhaseTime;
        var currentRotation = Vector3.Lerp(_startPosition, _risePosition, t);
        transform.rotation = Quaternion.Euler(currentRotation);
    }
}
