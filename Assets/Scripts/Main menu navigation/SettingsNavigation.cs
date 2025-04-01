using UnityEngine;
using UnityEngine.SceneManagement;
using VisualNovel_2025;
using UnityEngine.UI;
public class SettingsNavigation : MonoBehaviour
{
   private GameObject settingsPanel;
    private Scene Utilities;

    private void Start()
    {
        HelperClass helper = new HelperClass();
        Utilities = UnityEngine.SceneManagement.SceneManager.GetSceneByName("Utilities");
        settingsPanel = helper.GetGameObjectFromAnotherScene("Settings", Utilities);

        gameObject.GetComponent<Button>().onClick.AddListener(OpenSettings);
    }

    void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }
}
