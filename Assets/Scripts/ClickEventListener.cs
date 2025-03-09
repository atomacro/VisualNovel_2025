using UnityEngine;
using Yarn.Unity;

public class ClickEventListener : MonoBehaviour
{
    private bool isOptionView = false;
    private OptionsListView optionsListView;
    [SerializeField] LineView lineView;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] GameObject optionsListViewObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        optionsListView = optionsListViewObject.GetComponent<OptionsListView>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOptionViewState();
    }

    private void UpdateOptionViewState()
    {
        // Check if optionsListView is not null and if options are displayed
        if (optionsListView != null)
        {
            // Check if the optionsListView is active and visible
            isOptionView = optionsListView.gameObject.activeSelf && canvasGroup.alpha > 0;
            Debug.Log("Options Active");
        }
        else
        {
            isOptionView = false;
            Debug.Log("Options Inactive");
        }
    }
    public void ClickHandler()
    {

        if (isOptionView)
        {
            return;
        }

        if (lineView.useTypewriterEffect && lineView.lineText.maxVisibleCharacters < lineView.lineText.text.Length)
        {
            // Finish the typewriter effect by setting maxVisibleCharacters to the length of the text
            lineView.lineText.maxVisibleCharacters = lineView.lineText.text.Length;
        }
        else
        {
            lineView.UserRequestedViewAdvancement();
        }
    }
}