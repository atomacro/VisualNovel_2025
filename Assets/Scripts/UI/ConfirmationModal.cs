using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ConfirmationModal : MonoBehaviour
{
    private TextMeshProUGUI WarningMessage => GameObject.Find("WarningMessage").GetComponent<TextMeshProUGUI>();
    private Button ConfirmButton => GameObject.Find("Confirm").GetComponent<Button>();
    private Button CancelButton => GameObject.Find("Cancel").GetComponent<Button>();
    public string WarningMessageText { get; set; }
    public UnityEvent OnConfirmAction;
    public UnityEvent OnCancelAction;
    public void Start()
    {
        WarningMessage.text = WarningMessageText;
        ConfirmButton.onClick.AddListener(OnConfirm);
        CancelButton.onClick.AddListener(OnCancel);
    }
    public void DestroyConfimationModal()
    {
        Destroy(gameObject);
    }

    public void OnConfirm()
    {
        OnConfirmAction?.Invoke();
    }
    public void OnCancel()
    {
        OnCancelAction?.Invoke();
        DestroyConfimationModal();
    }
}


//    GameObject modalInstance = Instantiate(confirmationModalPrefab, canvasTransform);

//         // Set the modal's position to (0, 0)
//         RectTransform rectTransform = modalInstance.GetComponent<RectTransform>();
//         if (rectTransform != null)
//         {
//             rectTransform.anchoredPosition = Vector2.zero; // Set position to (0, 0)
//         }
//        ConfirmationModal confirmationModal = modalInstance.GetComponent<ConfirmationModal>();
