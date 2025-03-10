using UnityEngine;

public class SettingsNavigation : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }
}
