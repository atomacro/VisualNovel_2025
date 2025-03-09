using UnityEngine;

public class NewGameNavigation : MonoBehaviour
{
    public void NewGame()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("MainMenu");

    }
}
