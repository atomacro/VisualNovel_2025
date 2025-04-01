using UnityEngine;
using Yarn.Unity;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using VisualNovel_2025;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject lineViewObj;
    [SerializeField] private CanvasFader canvasFader;
    [SerializeField] private GameObject dialogueRunnerObject;
    [SerializeField] private GameObject dialogueTextObject;
    [SerializeField] private GameObject optionsListViewObject;
    [SerializeField] private AudioClip newGameClip;
    public List<string> DialogueLog = new List<string>();
    public List<string> Dialogues = new List<string>();
    private LineView lineView => lineViewObj.gameObject.GetComponent<LineView>();
    private OptionsListView optionsListView => optionsListViewObject.gameObject.GetComponent<OptionsListView>();
    private DialogueRunner dialogueRunner => dialogueRunnerObject.gameObject.GetComponent<DialogueRunner>();
    private CanvasGroup optionsListViewCanvavGroup => optionsListViewObject.gameObject.GetComponent<CanvasGroup>();
    private HelperClass helper = new HelperClass();
    private Scene Utilities => UnityEngine.SceneManagement.SceneManager.GetSceneByName("Utilities");


    private Pagination pagination;

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

    private IEnumerator StartDialogueAfterDelay()
    {
        // Wait a frame to ensure DialogueRunner is initialized
        yield return null;

        string startNode = "Scene1";
        if (PlayerPrefs.HasKey("LoadScene"))
        {
            startNode = PlayerPrefs.GetString("LoadScene", "Scene1");
            PlayerPrefs.SetString("LoadScene", "Scene1");
            PlayerPrefs.Save();
        }


        // Double-check that the DialogueRunner has nodes loaded
        if (dialogueRunner.NodeExists(startNode))
        {
            dialogueRunner.Stop();
            dialogueRunner.StartDialogue(startNode);
        }
    }
    private void Start()
    {
        lineView.typewriterEffectSpeed = PlayerPrefs.GetFloat("TextSpeed", 50);
        canvasFader.FadeIn(3f);
        StartCoroutine(StartDialogueAfterDelay());
        GameObject gallery = helper.GetGameObjectFromAnotherScene("Gallery", Utilities);
        GameObject pagination = helper.GetChildGameObject("Pagination", gallery);
        this.pagination = pagination.GetComponent<Pagination>();
    }

    private void Awake()
    {

        dialogueRunner.AddCommandHandler<string>("setbackgroundtrue", (name) => pagination.SetBackgroundValueTrue(name));
    }
}
// private void Awake()
// {
//     // string startNode = "";
//     // if (PlayerPrefs.HasKey("LoadScene"))
//     // {
//     //     startNode = PlayerPrefs.GetString("LoadScene", "Scene1");
//     //     PlayerPrefs.DeleteKey("LoadScene");
//     //     PlayerPrefs.Save();
//     // }
//     // else
//     // {
//     //     startNode = "Scene1";
//     // }

//     // dialogueRunner.Stop();
//     // dialogueRunner.StartDialogue(startNode);

// }



