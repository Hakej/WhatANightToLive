using Classes.Abstracts;
using Controllers;
using Handlers;
using UnityEngine;

namespace GameObjects
{
    public class Room : MonoBehaviour
    {
        public GameObject SecurityCamera;
        public Room[] AdjacentRooms;
        public bool IsCameraDisabled;

        private SecurityCamera _securityCameraScript;

        private void Awake()
        {
            if (IsCameraDisabled)
            {
                if (SecurityCamera != null)
                {
                    SecurityCamera.SetActive(false);
                }

                return;
            }

            _securityCameraScript = SecurityCamera.GetComponent<SecurityCamera>();

            EventHandler.Instance.OnEnemyChangingRoom += OnEnemyChangingRoom;
        }

        private void OnEnemyChangingRoom(Room oldRoom, Room newRoom)
        {
            if (IsCameraDisabled)
            {
                return;
            }

            if (this != oldRoom && this != newRoom)
            {
                return;
            }

            var curCam = SecurityCamerasController.Instance.CurrentCamera;

            if (curCam != SecurityCamera)
            {
                return;
            }

            _securityCameraScript.StartDistortion();
        }
    }
}
