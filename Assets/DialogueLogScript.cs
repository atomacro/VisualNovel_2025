using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class DialogueLogScript : MonoBehaviour
{
    [SerializeField] private GameObject GameManager;
    [SerializeField] private GameObject DialogueLogPanel;
    private List<string> dialogueLog = new List<string>();
    private TextMeshProUGUI dialogueText => DialogueLogPanel.GetComponent<TextMeshProUGUI>();
    void OnEnable()
    {
        dialogueText.text = "";
        dialogueLog = GameManager.GetComponent<GameManager>().getDialogueLog();
        foreach (string line in dialogueLog)
        {
            dialogueText.GetComponent<TextMeshProUGUI>().text += line + "\n\n";
        }
    }
}
