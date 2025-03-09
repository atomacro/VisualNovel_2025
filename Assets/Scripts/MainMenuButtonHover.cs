using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI textMeshPro; // Directly reference the text component

    private void Awake()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponentInChildren<TextMeshProUGUI>(); // Auto-assign if not set in Inspector
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textMeshPro.color = new Color32(255, 255, 225, 255); // Brighter color
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textMeshPro.color = new Color32(255, 255, 225, 100); // Dim color

    }
}
