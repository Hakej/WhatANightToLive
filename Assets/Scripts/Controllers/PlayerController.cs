using System.Collections;
using Handlers;
using UnityEngine;

namespace Controllers
{
    public class PlayerController : Singleton<PlayerController>
    {
        public float RotationSpeed = 0.5f;
        public float JumpscareRotationSpeed = 0.2f;

        public GameObject Player;
        public AudioSource MoveSound;

        private bool _isTurning;

        public void TurnLeft()
        {
            if (_isTurning) return;
            StartCoroutine(RotateMe(Vector3.up * -90, RotationSpeed));
        }

        public void TurnRight()
        {
            if (_isTurning) return;
            StartCoroutine(RotateMe(Vector3.up * 90, RotationSpeed));
        }

        public void TurnAround()
        {
            if (_isTurning) return;
            StartCoroutine(RotateMe(Vector3.up * 180, RotationSpeed));
        }

        private IEnumerator RotateMe(Vector3 byAngles, float rotationSpeed)
        {
            EventHandler.Instance.PlayerRotationStart();

            _isTurning = true;
            MoveSound.Play();

            var fromAngle = Player.transform.rotation;
            var toAngle = Quaternion.Euler(Player.transform.eulerAngles + byAngles);

            for (var t = 0f; t < 1f; t += Time.deltaTime / RotationSpeed)
            {
                Player.transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
                yield return null;
            }

            Player.transform.rotation = toAngle;
            _isTurning = false;

            EventHandler.Instance.PlayerRotationStop(toAngle);
        }

        public void RotateToFaceEnemy(Vector3 enemyPosition)
        {
            StopAllCoroutines();

            var direction = enemyPosition - Player.transform.position;
            var rotation = Quaternion.LookRotation(direction);
            var rotationAngle = rotation.eulerAngles - Player.transform.rotation.eulerAngles;

            StartCoroutine(RotateMe(rotationAngle, JumpscareRotationSpeed));
        }
    }
}