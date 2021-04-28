using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Singletons;
namespace Handlers
{
    public class SceneHandler : MonoBehaviour
    {
        [Header("Scenes")]
        public SceneAsset LoseScene;

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

        public void LoadScene(SceneAsset scene)
        {
            StartCoroutine(LoadLevel(scene.name));
        }

        private IEnumerator LoadLevel(string sceneName)
        {
            Transition.SetTrigger("Start");

            yield return new WaitForSeconds(_transitionTime);

            SceneManager.LoadScene(sceneName);
        }

        public void LoadSceneWithBlackScreen(SceneAsset scene)
        {
            StartCoroutine(LoadLevelWithBlackScreen(scene.name));
        }

        private IEnumerator LoadLevelWithBlackScreen(string sceneName)
        {
            BlackScreen.SetActive(true);

            yield return new WaitForSeconds(BlackScreenTime);

            SceneManager.LoadScene(sceneName);
        }
    }
}
