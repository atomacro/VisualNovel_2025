using System;
using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private bool isFromOtherScene = false;
    [SerializeField] private string objectName;
    private UtilityLoader utilityLoader;
    private void Awake()
    {
        if (isFromOtherScene)
        {
            utilityLoader = GameObject.Find("UtilityLoader").GetComponent<UtilityLoader>();
            MenuPanel = utilityLoader.getGameObject(objectName);
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