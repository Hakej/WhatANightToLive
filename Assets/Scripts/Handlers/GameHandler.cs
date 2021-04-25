using System.Collections.Generic;
using System.Linq;
using Assets.Classes.Unity;
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
        [TagSelector]
        public string EnemyAudioSourceTag = "";

        private void Start()
        {
            EventHandler.Instance.OnLose += OnLose;
            EventHandler.Instance.OnPowerToggle += OnPowerToggle;
        }

        private void OnLose()
        {
            var allAudioSources = FindObjectsOfType<AudioSource>();

            foreach (var audioSource in allAudioSources)
            {
                // Disable all audio sources, except for the enemies
                if (!audioSource.CompareTag(EnemyAudioSourceTag))
                {
                    audioSource.enabled = false;
                }
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
