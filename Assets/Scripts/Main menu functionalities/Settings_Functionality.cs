using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using Yarn.Unity;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class Settings_Functionality : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private ToggleGroup resolutionToggleGroup;
    [SerializeField] private ToggleGroup fullScreenToggleGroup;
    [SerializeField] private Slider textSpeedSlider;
    [SerializeField] private Button backButton;

    [SerializeField] private GameObject confirmModalPrefab;

    private GameObject SoundEffects;
    private GameObject BackgroundMusic;
    private GameObject UISounds;

    private Scene MainScene;
    private Scene Utilities;

    private GameObject lineView;
    private LineView lineViewScript;
    private Dictionary<string, int> PreviousSettings = new Dictionary<string, int>();
    private Dictionary<string, int> DefaultSettings = new Dictionary<string, int>();

    private void InitializeValues()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
        textSpeedSlider.value = PlayerPrefs.GetFloat("TextSpeed", 50);


        PreviousSettings["MasterVolume"] = PlayerPrefs.GetInt("MasterVolume", 1);
        PreviousSettings["MusicVolume"] = PlayerPrefs.GetInt("MusicVolume", 1);
        PreviousSettings["SFXVolume"] = PlayerPrefs.GetInt("SFXVolume", 1);
        PreviousSettings["TextSpeed"] = PlayerPrefs.GetInt("TextSpeed", 50);
        PreviousSettings["FullScreen"] = PlayerPrefs.GetInt("FullScreen", 1);
        PreviousSettings["ScreenWidth"] = PlayerPrefs.GetInt("ScreenWidth", Screen.currentResolution.width);
        PreviousSettings["ScreenHeight"] = PlayerPrefs.GetInt("ScreenHeight", Screen.currentResolution.height);

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

        bool isFullScreen = PlayerPrefs.GetInt("FullScreen", 1) == 1;
        Toggle[] toggles = fullScreenToggleGroup.GetComponentsInChildren<Toggle>();
        toggles[0].isOn = isFullScreen;
        toggles[1].isOn = !isFullScreen;
    }


    private GameObject GetGameObjectFromAnotherScene(String name, Scene scene)
    {
        foreach (GameObject obj in scene.GetRootGameObjects())
        {
            if (obj.name == name)
            {
                return obj;
            }
        }
        return null;
    }

    private void OnEnable()
    {
        //get game objects from other scenes
        MainScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName("MainScene");
        Utilities = UnityEngine.SceneManagement.SceneManager.GetSceneByName("Utilities");


        if (MainScene.isLoaded)
        {
            lineView = GetGameObjectFromAnotherScene("Line View", MainScene);
            Debug.Log("Main Scene Loaded");
        }
        if (Utilities.isLoaded)
        {
            BackgroundMusic = GetGameObjectFromAnotherScene("BackgroundMusic", Utilities);
            UISounds = GetGameObjectFromAnotherScene("UISounds", Utilities);
            SoundEffects = GetGameObjectFromAnotherScene("SoundEffects", Utilities);
            Debug.Log("Utilities Scene Loaded");
        }
        if (lineView != null)
        {
            lineViewScript = lineView.GetComponent<LineView>();
        }
        foreach (var item in DefaultSettings)
        {
            PreviousSettings[item.Key] = PlayerPrefs.GetInt(item.Key, item.Value);
        }
    }
    private void Start()
    {
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

        backButton.onClick.AddListener(BackButtonClicked);

        //initialize Default Settings
        DefaultSettings.Add("MasterVolume", 1);
        DefaultSettings.Add("MusicVolume", 1);
        DefaultSettings.Add("SFXVolume", 1);
        DefaultSettings.Add("TextSpeed", 50);
        DefaultSettings.Add("FullScreen", 1);
        DefaultSettings.Add("ScreenWidth", 1920);
        DefaultSettings.Add("ScreenHeight", 1080);

        //initialize previous settings
        foreach (var item in DefaultSettings)
        {
            PreviousSettings[item.Key] = PlayerPrefs.GetInt(item.Key, item.Value);
        }
    }
    private void OnMasterVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("MasterVolume", value);
        BackgroundMusic.GetComponent<AudioSource>().volume = value * PlayerPrefs.GetFloat("MusicVolume", 1);
        SoundEffects.GetComponent<AudioSource>().volume = value * PlayerPrefs.GetFloat("SFXVolume", 1);
        UISounds.GetComponent<AudioSource>().volume = value;
    }

    private void OnMusicVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        BackgroundMusic.GetComponent<AudioSource>().volume = value * PlayerPrefs.GetFloat("MasterVolume", 1);
    }

    private void OnSFXVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        SoundEffects.GetComponent<AudioSource>().volume = value * PlayerPrefs.GetFloat("MasterVolume", 1);
    }

    private void OnTextSpeedChanged(float value)
    {
        PlayerPrefs.SetFloat("TextSpeed", value);
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

    private void BackButtonClicked()
    {
        GameObject modalInstance = Instantiate(confirmModalPrefab);

        if (isChanged())
        {
            ConfirmationModal confirmationModal = modalInstance.GetComponent<ConfirmationModal>();
            confirmationModal.SetWarningMessage("Are you sure you want to exit without saving?");
            confirmationModal.OnConfirmAction.AddListener(() =>
            {
                ExitWithoutSaving(modalInstance);
            });
            confirmationModal.OnCancelAction.AddListener(() => Destroy(modalInstance));
        }
        else
        {
            Destroy(modalInstance);
            gameObject.SetActive(false);
        }
    }


    private bool isChanged()
    {
        foreach (var item in PreviousSettings)
        {
            if (PlayerPrefs.GetInt(item.Key, 0) != item.Value)
            {
                return true;
            }
        }
        return false;
    }

    private string[] getChangedSettings()
    {
        List<string> changedSettings = new List<string>();
        foreach (var item in PreviousSettings)
        {
            if (PlayerPrefs.GetInt(item.Key, 0) != item.Value)
            {
                changedSettings.Add(item.Key);
            }
        }
        return changedSettings.ToArray();
    }

    private void ExitWithoutSaving(GameObject modalInstance)
    {
        string[] changedSettings = getChangedSettings();
        foreach (var setting in changedSettings)
        {
            PlayerPrefs.SetInt(setting, PreviousSettings[setting]);
            Debug.Log(setting + PreviousSettings[setting]);
            if (setting == "TextSpeed")
            {
                if (lineView != null)
                {
                    lineViewScript.typewriterEffectSpeed = PreviousSettings[setting];
                }
            }
            if (setting == "FullScreen")
            {
                Screen.fullScreen = PreviousSettings[setting] == 1;
            }
            if (setting == "ScreenWidth" || setting == "ScreenHeight")
            {
                Screen.SetResolution(PreviousSettings["ScreenWidth"], PreviousSettings["ScreenHeight"], Screen.fullScreen);
            }
            if (setting == "MasterVolume" || setting == "MusicVolume" || setting == "SFXVolume")
            {
                BackgroundMusic.GetComponent<AudioSource>().volume = PreviousSettings["MasterVolume"] * PreviousSettings["MusicVolume"];
                SoundEffects.GetComponent<AudioSource>().volume = PreviousSettings["MasterVolume"] * PreviousSettings["SFXVolume"];
                UISounds.GetComponent<AudioSource>().volume = PreviousSettings["MasterVolume"];
            }
        }
        InitializeValues();
        Destroy(modalInstance);
        gameObject.SetActive(false);
        PlayerPrefs.Save();
    }
    public void SaveButtonClicked()
    {
        String[] changedSettings = getChangedSettings();
        foreach (var setting in changedSettings)
        {
            if (setting == "TextSpeed")
            {
                if (lineView != null)
                {
                    lineViewScript.typewriterEffectSpeed = PlayerPrefs.GetInt(setting, 50);
                }
            }
            if (setting == "FullScreen")
            {
                Screen.fullScreen = PlayerPrefs.GetInt("ScreenWidth", 1) == 1;
            }
            if (setting == "ScreenWidth" || setting == "ScreenHeight")
            {
                Screen.SetResolution(PlayerPrefs.GetInt("ScreenWidth", 1920), PlayerPrefs.GetInt("ScreenHeight", 1080), Screen.fullScreen);
            }
            if (setting == "MasterVolume" || setting == "MusicVolume" || setting == "SFXVolume")
            {
                BackgroundMusic.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MasterVolume", 1) * PlayerPrefs.GetFloat("MusicVolume", 1);
                SoundEffects.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MasterVolume", 1) * PlayerPrefs.GetFloat("SFXVolume", 1);
                UISounds.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MasterVolume", 1);
            }
        }
        PlayerPrefs.Save();

    }

    public void ResetValues()
    {
        foreach (var item in DefaultSettings)
        {
            PlayerPrefs.SetInt(item.Key, item.Value);
        }
        PlayerPrefs.Save();
        if (lineView != null)
        {
            lineViewScript.typewriterEffectSpeed = PreviousSettings["TextSpeed"];
        }
        Screen.SetResolution(PreviousSettings["ScreenWidth"], DefaultSettings["ScreenHeight"], DefaultSettings["FullScreen"] == 1);
        BackgroundMusic.GetComponent<AudioSource>().volume = DefaultSettings["MasterVolume"] * DefaultSettings["MusicVolume"];
        SoundEffects.GetComponent<AudioSource>().volume = DefaultSettings["MasterVolume"] * DefaultSettings["SFXVolume"];
        UISounds.GetComponent<AudioSource>().volume = DefaultSettings["MasterVolume"];
        InitializeValues();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButtonClicked();
        }
    }
}