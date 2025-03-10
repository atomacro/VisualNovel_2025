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
        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainScene");
    }
}