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

public class Save_Functionality : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TextMeshProUGUI chapterInfo;
    [SerializeField] private TextMeshProUGUI dateSaved;
    private DialogueRunner dialogueRunner;
    private SceneManager sceneManager;

    private void Start()
    {
        dialogueRunner = FindFirstObjectByType<DialogueRunner>();
        InitializeData();
    }

    [System.Serializable]
    public class SaveData
    {
        public string currentNode;
        public string backgroundImage;
        public string chapterNumber;
        public string chapterTitle;
        public string date;
    }

    private void InitializeData()
    {
        for (int i = 1; i <= 6; i++)
        {
            string filePath = GetSaveFilePath(i);

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                SaveData saveData = JsonUtility.FromJson<SaveData>(json);
                
                DisplaySaveData(i, saveData);
            }
            else
            {
                Debug.Log($"No save file found for slot {i}");
            }
        }
    }

    private void DisplaySaveData(int slotIndex, SaveData saveData)
    {
        // Load and display background image (if applicable)
        Sprite bgSprite = LoadBackgroundImage(saveData.backgroundImage);
        if (bgSprite != null) backgroundImage.sprite = bgSprite;

        // Set chapter and date text
        chapterInfo.text = $"Chapter {saveData.chapterNumber}: {saveData.chapterTitle}";
        dateSaved.text = $"Saved on: {saveData.date}";
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
        if (dialogueRunner == null) return;

        SaveData saveData = new SaveData();
        saveData.currentNode = dialogueRunner.CurrentNodeName;

        saveData.backgroundImage = sceneManager.GetCurrentBackground();

        saveData.chapterNumber = sceneManager.GetYarnVariable("$chapterNumber");
        saveData.chapterTitle = sceneManager.GetYarnVariable("$chapterTitle");
        saveData.date = DateTime.Now.ToString("MM/dd/yyyy");

        string filePath = GetSaveFilePath(saveSlot);
        File.WriteAllText(filePath, JsonUtility.ToJson(saveData, true));

        

        Debug.Log("Saved at" + filePath);
        Debug.Log("Saving game to slot: " + saveSlot);
    }

    public void LoadGame(int saveSlot)
    {
        if (dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.Stop();
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
}
