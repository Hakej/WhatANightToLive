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
            EventHandler.Instance.OnEnemySpawn += OnEnemySpawn;
        }

        private void OnEnemySpawn(Enemy enemy)
        {
            if (enemy.StartingRoom == this)
            {
                _securityCameraScript.StartDistortion();
            }
        }

        private void OnEnemyChangingRoom(Room oldRoom, Room newRoom)
        {
            if (SecurityCamera == null)
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
