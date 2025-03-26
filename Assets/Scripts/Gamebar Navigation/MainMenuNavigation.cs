using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuNavigation : MonoBehaviour
{
    [SerializeField] private GameObject MainCanvas;
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
    }
    private IEnumerator OpenMainMenu()
    {
        fader.FadeOut(1.5f);
        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("MainScene");
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainMenu", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
}