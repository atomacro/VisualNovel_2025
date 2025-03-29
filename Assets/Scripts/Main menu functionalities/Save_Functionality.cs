using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Yarn.Unity;
using Yarn;
using System.Drawing;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using System;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine.SceneManagement;
using VisualNovel_2025;

public class Save_Functionality : MonoBehaviour
{
    [Header("Save Slots")]
    [SerializeField] private UnityEngine.UI.Button[] saveSlotButtons = new UnityEngine.UI.Button[6];
    
    [Header("UI Elements")]
    [SerializeField] private Image[] backgroundImages = new Image[6];
    [SerializeField] private TextMeshProUGUI[] chapterInfoTexts = new TextMeshProUGUI[6];
    [SerializeField] private TextMeshProUGUI[] dateSavedTexts = new TextMeshProUGUI[6];
    
    private DialogueRunner dialogueRunner;
    [SerializeField] private GameObject menuPanel;
    private Scene MainScene;
    private SceneManager sceneManager;
    private HelperClass helper;

    [System.Serializable]
    public class SaveData
    {
        public string currentNode;
        public string backgroundImage;
        public string chapterNumber;
        public string chapterTitle;
        public string date;
    }

    private void Start()
    {
        helper = new HelperClass();
        InitializeData();
    }

    private void OnEnable()
    {
        MainScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName("MainScene");
    
        if(MainScene.isLoaded)
        {
            GameObject SceneManager = helper.GetGameObjectFromAnotherScene("SceneManager", MainScene);            
            sceneManager = SceneManager.GetComponent<SceneManager>();
            Debug.Log("Scene manager: "+ sceneManager != null);

            GameObject MainCanvas = helper.GetGameObjectFromAnotherScene("MainCanvas", MainScene);
            GameObject DialogueSystem = helper.GetChildGameObject("Dialogue System", MainCanvas);
            dialogueRunner = DialogueSystem.GetComponent<DialogueRunner>();
        }
        else
        {
            Debug.Log("Main Scene not loaded.");
        }
    }

    private void InitializeData()
    {
        for (int i = 0; i < saveSlotButtons.Length; i++)
        {
            int slotNumber = i + 1;

            saveSlotButtons[i].onClick.RemoveAllListeners();

            string filePath = GetSaveFilePath(i);

            if (File.Exists(GetSaveFilePath(slotNumber)))
            {
                int slot = slotNumber;  // Create a local copy for the lambda
                saveSlotButtons[i].onClick.AddListener(() => LoadGame(slot));
                
                LoadSlotData(slotNumber);
            }
            else
            {
                // If no save exists, clicking should save game
                int slot = slotNumber;  // Create a local copy for the lambda
                saveSlotButtons[i].onClick.AddListener(() => SaveGame(slot));
                
                // Reset UI to show empty slot
                Debug.Log($"No save file found for slot {i + 1}");
                ResetSlotUI(i);
            }
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
            
            // Set the background image if available
            if (!string.IsNullOrEmpty(saveData.backgroundImage))
            {
                Sprite bgSprite = LoadBackgroundSprite(saveData.backgroundImage);
                if (bgSprite != null)
                {
                    backgroundImages[index].sprite = bgSprite;
                    backgroundImages[index].color = UnityEngine.Color.white;
                }
                else
                {
                    Debug.LogError("Image file not found: " + filePath);
                }
            }
            
            // Set the chapter text
            chapterInfoTexts[index].text = $"Chapter {saveData.chapterNumber}: {saveData.chapterTitle}";
            
            // Set the date text
            dateSavedTexts[index].text = $"Saved on: {saveData.date}";
        }
    }

    private void ResetSlotUI(int index)
    {
        // Set default background or make it semi-transparent
        backgroundImages[index].color = new UnityEngine.Color(0.5f, 0.5f, 0.5f, 0.5f);
        chapterInfoTexts[index].text = "Empty Slot";
        dateSavedTexts[index].text = "";
    }

    private Sprite LoadBackgroundSprite(string backgroundPath)
    {
        try
        {
            // First try loading from Resources folder
            string resourcePath = backgroundPath.Replace("Resources/", "").Replace(".png", "");
            Sprite resourceSprite = Resources.Load<Sprite>(resourcePath);
            if (resourceSprite != null) return resourceSprite;
            
            // If not in resources, try loading from file
            if (File.Exists(backgroundPath))
            {
                Texture2D texture = new Texture2D(2, 2);
                byte[] imageBytes = File.ReadAllBytes(backgroundPath);
                if (imageBytes.Length > 0 && texture.LoadImage(imageBytes))
                {
                    return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error loading background image: {e.Message}");
        }
        
        return null;
    }
    

    private Sprite LoadBackgroundImage(string backgroundPath)
    {
        if (string.IsNullOrEmpty(backgroundPath)) return null;

        Texture2D texture = new Texture2D(2, 2);
        byte[] imageBytes = File.ReadAllBytes(backgroundPath);

        if (imageBytes.Length > 0 && texture.LoadImage(imageBytes))
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }

        return null;
    }

    public void SaveGame(int saveSlot)
    {
        Debug.Log("Saving...");
        if (dialogueRunner == null) 
        {
            Debug.Log("No dialoguerunner");
            return;
        }

        if (sceneManager == null)
        {
            Debug.LogError("SceneManager is NULL! Make sure it's assigned.");
            return;
        }

        string bg = sceneManager.GetCurrentBackground(); // This line is causing the error

        if (string.IsNullOrEmpty(bg))
        {
            Debug.LogError("Background image is NULL or EMPTY!");
        }

        SaveData saveData = new SaveData();
        saveData.currentNode = dialogueRunner.CurrentNodeName;

        saveData.backgroundImage = sceneManager.GetCurrentBackground();

        saveData.chapterNumber = sceneManager.GetYarnVariable("$chapterNumber");
        saveData.chapterTitle = sceneManager.GetYarnVariable("$chapterTitle");
        saveData.date = DateTime.Now.ToString("MM/dd/yyyy");

        string filePath = GetSaveFilePath(saveSlot);
        File.WriteAllText(filePath, JsonUtility.ToJson(saveData, true));

        RefreshAfterSave(saveSlot);

        Debug.Log("Saved at" + filePath);
        Debug.Log("Saving game to slot: " + saveSlot);
    }

    public void LoadGame(int saveSlot)
    {
        Debug.Log("Loading...");
        if (dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.Stop();
        }

        if (dialogueRunner == null) 
        {
            Debug.Log("No dialoguerunner");
            return;
        }

        string filePath = GetSaveFilePath(saveSlot);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            dialogueRunner.StartDialogue(saveData.currentNode);
        }

        Debug.Log("Loading game slot: " + saveSlot);
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

    private void RefreshAfterSave(int slotNumber)
    {
        int index = slotNumber - 1;  // Convert to 0-based index for arrays
        
        // Update the UI for this slot
        LoadSlotData(slotNumber);
        
        // Update the button behavior to be "load" instead of "save"
        saveSlotButtons[index].onClick.RemoveAllListeners();
        int slot = slotNumber;  // Create a local copy for the lambda
        saveSlotButtons[index].onClick.AddListener(() => LoadGame(slot));
    }
}
