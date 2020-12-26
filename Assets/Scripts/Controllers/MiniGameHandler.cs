using System.Collections;
using Effects;
using Handlers;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class MiniGameHandler : Singleton<MiniGameHandler>
    {
        [HideInInspector]
        public bool IsPlayerInMiniGame = false;

        public Camera PlayerCamera;
        
        public void StartMiniGame(GameObject miniGame)
        {
            UIHandler.Instance.TogglePlayerUI(false);
            
            PlayerCellphoneController.Instance.PhoneToggle();
            
            Instantiate(miniGame, transform);
            
            PlayerCamera.enabled = false;
            IsPlayerInMiniGame = true;
        }

        public void FinishMiniGame(GameObject miniGame)
        {
            StartCoroutine(FinishGameAfterDelay(miniGame, 2));
        }

        private IEnumerator FinishGameAfterDelay(GameObject miniGame, float delay)
        {
            Toast.Instance.Show("You've calmed your mind a little.", delay);

            yield return new WaitForSeconds(delay);
            
            UIHandler.Instance.TogglePlayerUI(true);
            
            Destroy(miniGame);
            
            PlayerCamera.enabled = true;
            IsPlayerInMiniGame = false;
        }
    }
}
