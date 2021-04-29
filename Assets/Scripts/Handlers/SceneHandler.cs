using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Singletons;

namespace Handlers
{
    public class SceneHandler : MonoBehaviour
    {
        [Header("Scenes")]
        public SceneReference MenuScene;
        public SceneReference GameplayScene;
        public SceneReference LoseScene;

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
            LoadScene(GameplayScene);
        }

        public void LoadMenuScene()
        {
            LoadScene(MenuScene);
        }

        private void OnLose()
        {
            LoadSceneWithBlackScreen(LoseScene);
        }

        private void OnDisable()
        {
            if (EventManager.Instance != null)
            {
                EventManager.Instance.OnLose -= OnLose;
            }
        }

        private void LoadScene(SceneReference sceneName)
        {
            StartCoroutine(LoadLevel(sceneName));
        }

        private IEnumerator LoadLevel(SceneReference sceneName)
        {
            Transition.SetTrigger("Start");

            yield return new WaitForSeconds(_transitionTime);

            SceneManager.LoadScene(sceneName);
        }

        private void LoadSceneWithBlackScreen(SceneReference scene)
        {
            StartCoroutine(LoadLevelWithBlackScreen(scene));
        }

        private IEnumerator LoadLevelWithBlackScreen(SceneReference scene)
        {
            BlackScreen.SetActive(true);

            yield return new WaitForSeconds(BlackScreenTime);

            SceneManager.LoadScene(scene);
        }
    }
}
