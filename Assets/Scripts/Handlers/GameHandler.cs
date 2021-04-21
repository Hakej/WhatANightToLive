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
        public bool IsPowerOn = true;
        public GameObject PlayerPowerSwitch;

        [Header("Lose")]
        public GameObject[] GameObjectsToDisableOnLose;

        private void Start()
        {
            EventHandler.Instance.OnLose += OnLose;
            EventHandler.Instance.OnPowerToggle += OnPowerToggle;
        }

        private void OnLose()
        {
            foreach (var gameObject in GameObjectsToDisableOnLose)
            {
                gameObject.SetActive(false);
            }

            var allAudioSources = FindObjectsOfType<AudioSource>();

            foreach (var audioSource in allAudioSources)
            {
                audioSource.enabled = false;
            }

            SceneHandler.LoadSceneWithBlackScreen(LoseScene);
        }

        private void OnPowerToggle(bool areLightsOn, string poweredTag)
        {
            if (!PlayerPowerSwitch.CompareTag(poweredTag))
            {
                return;
            }

            IsPowerOn = areLightsOn;
        }
    }
}
