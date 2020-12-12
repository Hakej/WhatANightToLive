using System.Collections;
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
        public AudioSource MiniGameFinish;
        public TextMeshProUGUI GainedSanityText;
        
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
            GainedSanityText.gameObject.SetActive(true);
            GainedSanityText.text = "You've calmed your mind a little.";
            
            MiniGameFinish.Play();
            
            yield return new WaitForSeconds(delay);
            
            GainedSanityText.gameObject.SetActive(false);
            
            UIHandler.Instance.TogglePlayerUI(true);
            
            Destroy(miniGame);
            
            PlayerCamera.enabled = true;
            IsPlayerInMiniGame = false;
        }
    }
}
