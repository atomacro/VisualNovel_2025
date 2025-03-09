using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NewGameNavigation : MonoBehaviour
{
    public void NewGame()
    {
        StartCoroutine(LoadMainSceneAsync());
    }

    private IEnumerator LoadMainSceneAsync()
    {
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainScene");
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("MainMenu");

        while (!asyncLoad.isDone)
        {
            Debug.Log(asyncLoad.progress);
            yield return null;
        }
    }
}