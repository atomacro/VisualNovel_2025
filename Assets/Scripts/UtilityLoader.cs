using UnityEngine;
using UnityEngine.SceneManagement;

public class UtilityLoader : MonoBehaviour
{
    private Scene UtilityScene => UnityEngine.SceneManagement.SceneManager.GetSceneByName("Utilities");

    void Start()
    {
        if (UtilityScene.isLoaded == false)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Utilities", LoadSceneMode.Additive);
        }
        else
        {
            return;
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
