using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NewGameNavigation : MonoBehaviour
{

    [SerializeField] private CanvasFader fader;
    public void NewGame()
    {
        StartCoroutine(LoadMainSceneAsync());
    }

    private IEnumerator LoadMainSceneAsync()
    {
        fader.FadeOut(1.5f);
        yield return new WaitForSeconds(3f);
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("MainMenu");
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);
    }
}