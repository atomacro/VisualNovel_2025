using UnityEngine;
using Yarn;
using System.Collections.Generic;
using TMPro;

public class DialogueLogScript : MonoBehaviour
{
    [SerializeField] private GameObject GameManager;
    [SerializeField] private GameObject DialogueLogPanel;
    private List<string> dialogueLog = new List<string>();
    void Start()
    {
        dialogueLog = GameManager.GetComponent<GameManager>().getDialogueLog();
        foreach (string line in dialogueLog)
        {
            DialogueLogPanel.GetComponent<TextMeshProUGUI>().text += line + "\n\n";
        }
    }


}
