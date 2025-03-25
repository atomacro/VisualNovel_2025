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

public class Save_Functionality : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TextMeshProUGUI chapterInfo;
    [SerializeField] private TextMeshProUGUI dateSaved;
    private DialogueRunner dialogueRunner;

    private void Start()
    {
        dialogueRunner = FindFirstObjectByType<DialogueRunner>();
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

    // private void InitializeData(){
        
    // }

    public void SaveGame(int saveSlot)
    {
        if (dialogueRunner == null) return;

        SaveData saveData = new SaveData();
        saveData.currentNode = dialogueRunner.CurrentNodeName;

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
