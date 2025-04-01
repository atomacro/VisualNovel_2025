using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VisualNovel_2025;

public class MainMenuNavigation : MonoBehaviour
{
    [SerializeField] private GameObject MainCanvas;
    private Scene Utility => UnityEngine.SceneManagement.SceneManager.GetSceneByName("Utilities");
    private HelperClass helper = new HelperClass();
    private CanvasFader fader => MainCanvas.GetComponent<CanvasFader>();


    private void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }


    private void OnClick()
    {
        fader.StopFade();
        StartCoroutine(OpenMainMenu());
        AudioSource BackgroundMusic = helper.GetGameObjectFromAnotherScene("BackgroundMusic", Utility).GetComponent<AudioSource>();
        if (BackgroundMusic != null)
        {
            BackgroundMusic.Stop();
        }
    }
    private IEnumerator OpenMainMenu()
    {
        fader.FadeOut(1.5f);
        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("MainScene");
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainMenu", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
}