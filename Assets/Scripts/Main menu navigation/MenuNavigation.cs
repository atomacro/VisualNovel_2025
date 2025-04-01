using System;
using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private bool isFromOtherScene = false;
    [SerializeField] private string objectName;
    private UtilityLoader utilityLoader => GameObject.Find("UtilityLoader").GetComponent<UtilityLoader>();
    private void Awake()
    {
        if (isFromOtherScene)
        {
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