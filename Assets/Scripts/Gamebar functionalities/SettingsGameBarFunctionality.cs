using UnityEngine;
using UnityEngine.UI;

public class SettingsGameBarFunctionality : MonoBehaviour
{
    private UtilityLoader utilityLoader => GameObject.Find("UtilityLoader").GetComponent<UtilityLoader>();
    private GameObject settingsPanel => utilityLoader.getGameObject("Settings");

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OpenSettings);
    }

    void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

}
