using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using Yarn.Unity;

public class Settings_Functionality : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private ToggleGroup resolutionToggleGroup;
    [SerializeField] private ToggleGroup fullScreenToggleGroup;
    [SerializeField] private Slider textSpeedSlider;
    [SerializeField] private Button backButton;


    [SerializeField] private GameObject lineView;
    private LineView lineViewScript;


    private void InitializeValues()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
        textSpeedSlider.value = PlayerPrefs.GetFloat("TextSpeed", 50);

        int savedWidth = PlayerPrefs.GetInt("ScreenWidth", Screen.currentResolution.width);
        int savedHeight = PlayerPrefs.GetInt("ScreenHeight", Screen.currentResolution.height);
        foreach (var toggle in resolutionToggleGroup.GetComponentsInChildren<Toggle>())
        {
            string toggleText = toggle.GetComponentInChildren<TextMeshProUGUI>().text;
            string[] resolutionValues = toggleText.Split('x');
            int width = int.Parse(resolutionValues[0]);
            int height = int.Parse(resolutionValues[1]);
            if (width == savedWidth && height == savedHeight)
            {
                toggle.isOn = true;
                break;
            }
        }

        foreach (var toggle in fullScreenToggleGroup.GetComponentsInChildren<Toggle>())
        {
            string toggleText = toggle.GetComponentInChildren<TextMeshProUGUI>().text;
            if (PlayerPrefs.GetInt("FullScreen", 1) == 1)
            {
                Screen.fullScreen = true;
                toggle.isOn = toggleText == "Fullscreen Mode";
                break;
            }
        }
    }

    private void Awake()
    {
        InitializeValues();
    }

    private void Start()
    {
        if (lineView != null)
        {
            lineViewScript = lineView.GetComponent<LineView>();
        }

        //initialize values when opening
        InitializeValues();

        //attach event listeners
        masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        textSpeedSlider.onValueChanged.AddListener(OnTextSpeedChanged);

        foreach (var toggle in fullScreenToggleGroup.GetComponentsInChildren<Toggle>())
        {
            toggle.onValueChanged.AddListener(OnFullScreenToggleChanged);
        }

        foreach (var toggle in resolutionToggleGroup.GetComponentsInChildren<Toggle>())
        {
            toggle.onValueChanged.AddListener(OnResolutionToggleChanged);
        }

        backButton.onClick.AddListener(backButtonClicked);

    }
    private void OnMasterVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("MasterVolume", value);
        Debug.Log("Master Volume changed: " + value);
    }

    private void OnMusicVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        Debug.Log("Music Volume changed: " + value);
    }

    private void OnSFXVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        Debug.Log("SFX Volume changed: " + value);
    }

    private void OnTextSpeedChanged(float value)
    {
        PlayerPrefs.SetFloat("TextSpeed", value);
        Debug.Log("Text Speed changed: " + value);
        if (lineViewScript != null)
        {
            lineViewScript.typewriterEffectSpeed = value;
        }
    }


    private void OnFullScreenToggleChanged(bool isOn)
    {
        Toggle fullScreen = fullScreenToggleGroup.ActiveToggles().FirstOrDefault();
        if (fullScreen != null)
        {
            string fullScreenString = fullScreen.GetComponentInChildren<TextMeshProUGUI>().text;
            bool isFullScreen = fullScreenString == "Fullscreen Mode";
            Screen.fullScreen = isFullScreen;
            Debug.Log("Full Screen changed: " + isFullScreen);
            PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
        }
    }
    private void OnResolutionToggleChanged(bool isOn)
    {
        Toggle resolution = resolutionToggleGroup.ActiveToggles().FirstOrDefault();
        if (resolution != null)
        {
            string resolutionString = resolution.GetComponentInChildren<TextMeshProUGUI>().text;
            string[] resolutionValues = resolutionString.Split('x');
            int width = int.Parse(resolutionValues[0]);
            int height = int.Parse(resolutionValues[1]);
            Screen.SetResolution(width, height, Screen.fullScreen);
            Debug.Log("Resolution changed: " + width + "x" + height);
            PlayerPrefs.SetInt("ScreenWidth", width);
            PlayerPrefs.SetInt("ScreenHeight", height);
        }
    }

    private void backButtonClicked()
    {
        PlayerPrefs.Save();
        gameObject.SetActive(false);

    }
}