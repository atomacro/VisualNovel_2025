using UnityEngine;
using UnityEngine.UI;

public class SettingsGameBarFunctionality : MonoBehaviour
{
    [SerializeField] private GameObject SettingsPanel;

    private void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OpenSettings);
    }

    void OpenSettings()
    {
        SettingsPanel.SetActive(true);
    }

}
