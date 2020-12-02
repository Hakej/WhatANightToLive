using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator Transition;

    public float TransitionTime = 1f;
    
    public void LoadWinScene()
    {
        StartCoroutine(LoadLevel("Scenes/WinScene"));
    }
    
    public void LoadLoseScene()
    {
        StartCoroutine(LoadLevel("Scenes/LoseScene"));
    }
    private IEnumerator LoadLevel(string sceneName)
    {
        Transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(TransitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
