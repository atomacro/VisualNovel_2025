using System;
using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField] private GameObject MenuPanel;

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
