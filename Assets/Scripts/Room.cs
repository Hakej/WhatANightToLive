using Controllers;
using Handlers;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject SecurityCamera;
    public GameObject[] AdjacentRooms;

    private SecurityCamera _securityCameraScript;

    private void Start()
    {
        var secCam = gameObject.transform.Find("SecurityCamera");

        if (secCam == null)
        {
            return;
        }

        SecurityCamera = secCam.gameObject;
        _securityCameraScript = SecurityCamera.GetComponent<SecurityCamera>();
            
        EventHandler.Instance.OnEnemyChangingRoom += OnEnemyChangingRoom;
    }

    private void OnEnemyChangingRoom(GameObject oldRoom, GameObject newRoom)
    {
        if (gameObject != oldRoom && gameObject != newRoom)
        {
            return;
        }

        var curCam = SecurityCamerasController.Instance.Cameras.Current;

        if (curCam != SecurityCamera)
        {
            return;
        }
        
        _securityCameraScript.StartDistortion();
    }
}
