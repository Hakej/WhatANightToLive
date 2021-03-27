using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class PlayerController : Singleton<PlayerController>
    {
        public float RotationSpeed = 0.5f;

        public GameObject Player;
        public GameObject MovementUI;
        public GameObject FlashlightButton;
        public AudioSource MoveSound;

        private bool _isTurning;

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
            MovementUI.SetActive(false);
            MoveSound.Play();

            var fromAngle = Player.transform.rotation;
            var toAngle = Quaternion.Euler(Player.transform.eulerAngles + byAngles);

            for (var t = 0f; t < 1; t += Time.deltaTime / RotationSpeed)
            {
                Player.transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
                yield return null;
            }

            Player.transform.rotation = toAngle;
            MovementUI.SetActive(true);
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
    }
}