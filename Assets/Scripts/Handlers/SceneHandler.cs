using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Singletons;
using UnityEditor;

namespace Handlers
{
    public class SceneHandler : MonoBehaviour
    {
        [Header("Scenes")]
        public string MenuSceneName;
        public string GameplaySceneName;
        public string LoseSceneName;

        [Header("Transition")]
        public Animator Transition;
        public GameObject BlackScreen;
        public float BlackScreenTime = 6f;

        private float _transitionTime;

        public void Start()
        {
            _transitionTime = Transition.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;

            EventManager.Instance.OnLose += OnLose;
        }

        public void LoadGameplayScene()
        {
            LoadScene(GameplaySceneName);
        }

        public void LoadMenuScene()
        {
            LoadScene(MenuSceneName);
        }

        private void OnLose()
        {
            LoadSceneWithBlackScreen(LoseSceneName);
        }

        private void OnDisable()
        {
            if (EventManager.Instance != null)
            {
                EventManager.Instance.OnLose -= OnLose;
            }
        }

        private void LoadScene(string sceneName)
        {
            StartCoroutine(LoadLevel(sceneName));
        }

        private IEnumerator LoadLevel(string sceneName)
        {
            Transition.SetTrigger("Start");

            yield return new WaitForSeconds(_transitionTime);

            SceneManager.LoadScene(sceneName);
        }

        private void LoadSceneWithBlackScreen(string sceneName)
        {
            StartCoroutine(LoadLevelWithBlackScreen(sceneName));
        }

        private IEnumerator LoadLevelWithBlackScreen(string sceneName)
        {
            BlackScreen.SetActive(true);

            yield return new WaitForSeconds(BlackScreenTime);

            SceneManager.LoadScene(sceneName);
        }
    }
}
