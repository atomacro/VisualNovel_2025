using UnityEngine;
using UnityEngine.SceneManagement;
using VisualNovel_2025;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private bool isFromOtherScene = false;
    [SerializeField] private string objectName;
    private Scene Utilities;
    private HelperClass helper;
    private void Awake()
    {
        if (helper == null)
        {
            helper = new HelperClass();
        }
        Utilities = UnityEngine.SceneManagement.SceneManager.GetSceneByName("Utilities");


        if (isFromOtherScene)
        {
            MenuPanel = helper.GetGameObjectFromAnotherScene(objectName, Utilities);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMenu();
        }
    }
    public void OpenMenu()
    {
        MenuPanel.SetActive(true);
    }

    public void CloseMenu()
    {
        MenuPanel.SetActive(false);
    }
}