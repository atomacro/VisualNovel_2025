using UnityEngine;
using Yarn.Unity;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject lineViewObj;
    [SerializeField] private CanvasFader canvasFader;
    [SerializeField] private GameObject dialogueRunnerObject;
    [SerializeField] private GameObject dialogueTextObject;
    [SerializeField] private GameObject optionsListViewObject;
    public List<string> DialogueLog = new List<string>();
    public List<string> Dialogues = new List<string>();
    private LineView lineView => lineViewObj.gameObject.GetComponent<LineView>();
    private OptionsListView optionsListView => optionsListViewObject.gameObject.GetComponent<OptionsListView>();
    private DialogueRunner dialogueRunner => dialogueRunnerObject.gameObject.GetComponent<DialogueRunner>();
    private CanvasGroup optionsListViewCanvavGroup => optionsListViewObject.gameObject.GetComponent<CanvasGroup>();

    private bool isLogged = false;

    public List<string> getDialogueLog()
    {
        return DialogueLog;
    }

    public void setDialogueLog(List<string> dialogueLog)
    {
        DialogueLog = dialogueLog;
    }
    public void Update()
    {

        if (lineView.useTypewriterEffect && !(lineView.lineText.maxVisibleCharacters < lineView.lineText.text.Length) && !isLogged)
        {
            string line = "";
            if (lineView.characterNameText.text != "" && lineView.characterNameText.text != "Character Name" && lineView.characterNameText.text != null)
            {
                line += lineView.characterNameText.text + ": ";
            }
            line += lineView.lineText.text;
            DialogueLog.Add(line);
            Dialogues.Add(lineView.lineText.text);
            isLogged = true;
        }
        else
        {
            isLogged = Dialogues.FindIndex(x => x == lineView.lineText.text) != -1;
        }
    }
    private void Start()
    {
        lineView.typewriterEffectSpeed = PlayerPrefs.GetFloat("TextSpeed", 50);
        canvasFader.FadeIn(3f);
    }
}
