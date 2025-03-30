using UnityEngine;
using System.IO;
using Yarn.Unity;
using Yarn;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using VisualNovel_2025;

public class Save_Functionality : MonoBehaviour
{
    [Header("Save Slots")]
    [SerializeField] private Button[] saveSlotButtons = new Button[6];

    [Header("UI Elements")]
    [SerializeField] private Image[] backgroundImages = new Image[6];
    [SerializeField] private TextMeshProUGUI[] chapterInfoTexts = new TextMeshProUGUI[6];
    [SerializeField] private TextMeshProUGUI[] dateSavedTexts = new TextMeshProUGUI[6];

    [SerializeField] private bool isSaveScreen;
    [SerializeField] private GameObject menuPanel;
    private DialogueRunner dialogueRunner;
    
    private Scene MainScene;
    private SceneManager sceneManager;
    private HelperClass helper = new HelperClass();

    [System.Serializable]
    public class SaveData
    {
        public string currentNode;
        public string backgroundImage;
        public string chapterNumber;
        public string chapterTitle;
        public string date;
    }

    private void OnEnable()
    {
        MainScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName("MainScene");

        if (MainScene.isLoaded)
        {
            GameObject SceneManager = helper.GetGameObjectFromAnotherScene("SceneManager", MainScene);
            sceneManager = SceneManager.GetComponent<SceneManager>();

            GameObject MainCanvas = helper.GetGameObjectFromAnotherScene("MainCanvas", MainScene);
            GameObject DialogueSystem = helper.GetChildGameObject("Dialogue System", MainCanvas);
            dialogueRunner = DialogueSystem.GetComponent<DialogueRunner>();
        }
        else
        {
            Debug.LogWarning("Main Scene not loaded.");
        }
        InitializeData(isSaveScreen);
    }

    private void InitializeData(bool isSaveScreen)
    {
        for (int i = 0; i < saveSlotButtons.Length; i++)
        {
            int slotNumber = i + 1;

            saveSlotButtons[i].onClick.RemoveAllListeners();

            string filePath = GetSaveFilePath(i);

            if (File.Exists(GetSaveFilePath(slotNumber)))
            {
                LoadSlotData(slotNumber);
            }
            else
            {
                // Debug.Log($"No save file found for slot {i + 1}");
                ResetSlotUI(i);
            }

            int slot = slotNumber;

            saveSlotButtons[i].onClick.AddListener(() => {
                if(isSaveScreen) SaveGame(slot);
                else LoadGame(slot);
            });
        }
    }

    private void LoadSlotData(int slotNumber)
    {
        int index = slotNumber - 1;  // Convert to 0-based index for arrays
        string filePath = GetSaveFilePath(slotNumber);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            if (!string.IsNullOrEmpty(saveData.backgroundImage))
            {
                string backgroundPath = saveData.backgroundImage;
                backgroundImages[index].sprite = LoadBackgroundImage(backgroundPath);
                backgroundImages[index].color = Color.white;
            }

            chapterInfoTexts[index].text = $"Chapter {saveData.chapterNumber}: {saveData.chapterTitle}";
            dateSavedTexts[index].text = $"Saved on: {saveData.date}";
        }
    }

    private void ResetSlotUI(int index)
    {
        // Set default background or make it semi-transparent
        backgroundImages[index].color = new Color(1f, 1f, 1f, 1f);
        chapterInfoTexts[index].text = "Empty Slot";
        dateSavedTexts[index].text = "";
    }

    private Sprite LoadBackgroundImage(string backgroundPath)
    {
        return sceneManager.GetBackgroundByName(backgroundPath);
    }

    public void SaveGame(int slotNumber)
    {
        if (dialogueRunner == null)
        {
            Debug.LogWarning("SAVE ERROR: No dialoguerunner");
            return;
        }

        if (sceneManager == null)
        {
            Debug.LogError("SceneManager is NULL! Make sure it's assigned.");
            return;
        }

        string bg = sceneManager.GetCurrentBackground();

        if (string.IsNullOrEmpty(bg))
        {
            Debug.LogError("Background image is NULL or EMPTY!");
        }

        SaveData saveData = new SaveData();
        // Save the data
        saveData.currentNode = dialogueRunner.CurrentNodeName;
        saveData.backgroundImage = sceneManager.GetCurrentBackground();
        saveData.chapterNumber = sceneManager.GetYarnVariable("$chapterNumber");
        saveData.chapterTitle = sceneManager.GetYarnVariable("$chapterTitle");
        saveData.date = DateTime.Now.ToString("MM/dd/yyyy");

        string filePath = GetSaveFilePath(slotNumber);
        File.WriteAllText(filePath, JsonUtility.ToJson(saveData, true));

        LoadSlotData(slotNumber);

        Debug.Log("Saved at" + filePath);
        Debug.Log("Saving game to slot: " + slotNumber);
    }

    public void LoadGame(int slotNumber)
    {
        if (dialogueRunner == null)
        {
            return;
        }

        if (dialogueRunner.IsDialogueRunning) dialogueRunner.Stop();
    
        string filePath = GetSaveFilePath(slotNumber);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            dialogueRunner.StartDialogue(saveData.currentNode);
        }
        Debug.Log("Loading game slot: " + slotNumber);
        menuPanel.SetActive(false);
    }

    string GetSaveFilePath(int slotNumber)
    {
        string directoryPath = Application.persistentDataPath + "/Saves";

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        return Path.Combine(directoryPath, $"save_{slotNumber}.json");
    }
}
