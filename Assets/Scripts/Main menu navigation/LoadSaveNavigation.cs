using System;
using UnityEngine;

public class LoadSaveNavigation : MonoBehaviour
{
    [SerializeField] private GameObject loadMenuPanel;
    public void OpenLoadMenu()
    {
        Console.WriteLine("Gumana!!");
        loadMenuPanel.SetActive(true);
    }
    
    public void CloseLoadMenu()
    {
        loadMenuPanel.SetActive(false);
    }
}
