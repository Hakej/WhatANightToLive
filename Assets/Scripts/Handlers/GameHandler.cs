using Assets.Classes.Unity;
using UnityEngine;
using Singletons;

namespace Handlers
{
    public class GameHandler : MonoBehaviour
    {

        [Header("Lose")]
        [TagSelector]
        public string EnemyAudioSourceTag = "";

        private void Start()
        {
            EventManager.Instance.OnLose += OnLose;
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
        }
    }
}
