using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class PlayerController : Singleton<PlayerController>
    {
        public float RotationSpeed = 0.5f;
        public float JumpscareRotationSpeed = 0.2f;

        public GameObject Player;
        public GameObject PlayerUI;
        public GameObject FlashlightButton;
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
            _isTurning = true;
            PlayerUI.SetActive(false);
            MoveSound.Play();

            var fromAngle = Player.transform.rotation;
            var toAngle = Quaternion.Euler(Player.transform.eulerAngles + byAngles);

            for (var t = 0f; t < 1f; t += Time.deltaTime / RotationSpeed)
            {
                Player.transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
                yield return null;
            }

            Player.transform.rotation = toAngle;
            PlayerUI.SetActive(true);
            _isTurning = false;

            if ((int)toAngle.eulerAngles.y != 270)
            {
                FlashlightButton.SetActive(true);
            }
            else
            {
                FlashlightButton.SetActive(false);
            }
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