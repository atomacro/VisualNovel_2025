using UnityEngine;

public class NewGameNavigation : MonoBehaviour
{
    public void NewGame()
    {

        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainScene");
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("MainMenu");

    }
}
