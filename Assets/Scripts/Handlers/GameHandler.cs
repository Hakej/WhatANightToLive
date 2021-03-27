using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Handlers
{
    public class GameHandler : Singleton<GameHandler>
    {
        [Header("Scenes")]
        public SceneHandler SceneHandler;
        public SceneAsset WinScene;
        public SceneAsset LoseScene;

        [Header("Player's Room's Power")]
        public bool IsPlayersPowerOn = true;
        public GameObject PlayerPowerSwitch;

        [Header("Objects to destroy on finish")]
        public List<GameObject> ObjectsToDestroyOnFinish;

        [Header("Object to activate on win")]
        public GameObject EndScreen;

        [HideInInspector]
        public int CurrentDangerLevel = 1;

        private void Start()
        {
            EventHandler.Instance.OnLose += OnLose;
            EventHandler.Instance.OnPowerToggle += OnPowerToggle;
        }

        private void OnLose()
        {
            // TODO: Add losing logic
            SceneHandler.LoadScene(LoseScene);
        }

        private void OnPowerToggle(bool areLightsOn, string poweredTag)
        {
            if (!PlayerPowerSwitch.CompareTag(poweredTag))
            {
                return;
            }

            IsPlayersPowerOn = areLightsOn;

            if (areLightsOn)
            {
                SanityHandler.Instance.CurrentFearLevel--;
                CurrentDangerLevel++;
            }
            else
            {
                SanityHandler.Instance.CurrentFearLevel++;
                CurrentDangerLevel--;
            }
        }
    }
}
