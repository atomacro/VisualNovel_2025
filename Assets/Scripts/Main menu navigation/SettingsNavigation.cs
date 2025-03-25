using UnityEngine;

public class SettingsNavigation : MonoBehaviour
{
    private UtilityLoader utilityLoader => GameObject.Find("UtilityLoader").GetComponent<UtilityLoader>();
    private GameObject settingsPanel => utilityLoader.getGameObject("Settings");
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }
}
