using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
public class SkipGameBarFunctionality : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    private string nextNodeName;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(SkipToNextNode);

    }

    [YarnCommand("setNextNode")]
    public void SetNextNode(string nodeName)
    {
        nextNodeName = nodeName;
    }
    
    public void SkipToNextNode()
    {
        if (dialogueRunner != null && !string.IsNullOrEmpty(nextNodeName))
        {
            dialogueRunner.Stop();
            dialogueRunner.StartDialogue(nextNodeName);
        }
    }
}
