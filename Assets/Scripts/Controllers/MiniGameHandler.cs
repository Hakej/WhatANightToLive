using Handlers;
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
            UIHandler.Instance.TogglePlayerUI(true);
            
            Destroy(miniGame);
            
            PlayerCamera.enabled = true;
            IsPlayerInMiniGame = false;
        }
    }
}
