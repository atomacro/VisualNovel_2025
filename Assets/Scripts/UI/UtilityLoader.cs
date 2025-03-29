using UnityEngine;
using UnityEngine.SceneManagement;

public class UtilityLoader : MonoBehaviour
{
    private Scene UtilityScene;
    public void Awake()
    {
        UtilityScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName("Utilities");

        if (!UtilityScene.isLoaded)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Utilities", LoadSceneMode.Additive);
        }
    }
    public GameObject getGameObject(string gameObjectName)
    {
        foreach (GameObject obj in UtilityScene.GetRootGameObjects())
        {
            if (obj.name == gameObjectName)
            {
                return obj;
            }
        }
        return null;
    }
}
