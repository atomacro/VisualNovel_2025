using UnityEngine;
using Yarn;
using Yarn.Unity;
using System.Collections.Generic;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject lineViewObj;
    [SerializeField] private CanvasFader canvasFader;
    [SerializeField] private GameObject dialogueRunnerObject;
    [SerializeField] private GameObject dialogueTextObject;
    public List<string> DialogueLog = new List<string>();
    public List<string> Dialogues = new List<string>();
    private LineView lineView => lineViewObj.gameObject.GetComponent<LineView>();

    private bool isLogged = false;

    public List<string> getDialogueLog()
    {
        return DialogueLog;
    }
    public void Update()
    {

        if (lineView.useTypewriterEffect && !(lineView.lineText.maxVisibleCharacters < lineView.lineText.text.Length) && !isLogged)
        {
            string line = "";
            if (lineView.characterNameText.text != ": " && lineView.characterNameText.text != "Character Name: ")
            {
                line += lineView.characterNameText.text + ": ";
            }
            line += lineView.lineText.text;
            DialogueLog.Add(line);
            Dialogues.Add(lineView.lineText.text);
            Debug.Log(line);
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
