using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HideGameBarFunctionality : MonoBehaviour
{
    [SerializeField] GameObject dialogueSystem;
    private bool isHidden = false;
    private CanvasGroup canvasGroup;
    public void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ToggleHide);
        canvasGroup = dialogueSystem.GetComponent<CanvasGroup>();
    }
    private void ToggleHide()
    {
        isHidden = !isHidden;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = isHidden ? "Unhide" : "Hide";
        canvasGroup.alpha = isHidden ? 0 : 1;
        canvasGroup.interactable = !isHidden;
        canvasGroup.blocksRaycasts = !isHidden;
    }
}
