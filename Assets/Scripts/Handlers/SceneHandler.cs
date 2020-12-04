using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Handlers
{
    public class SceneHandler : MonoBehaviour
    {
        public Animator Transition;

        private float _transitionTime;

        public void Start()
        {
            _transitionTime = Transition.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
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
    }
}
