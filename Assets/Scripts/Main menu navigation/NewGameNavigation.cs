using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using VisualNovel_2025;

public class NewGameNavigation : MonoBehaviour
{

    [SerializeField] private CanvasFader fader;
    [SerializeField] private AudioClip newGameClip;
    private Scene Utilities => UnityEngine.SceneManagement.SceneManager.GetSceneByName("Utilities");
    private HelperClass helper = new HelperClass();

    public void onClick()
    {
        AudioSource UISounds = helper.GetGameObjectFromAnotherScene("UISounds", Utilities).GetComponent<AudioSource>();
        UISounds.clip = newGameClip;
        UISounds.Play();
    }
    public void NewGame()
    {
        PlayerPrefs.SetString("LoadScene", "Scene1");
        PlayerPrefs.Save();
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