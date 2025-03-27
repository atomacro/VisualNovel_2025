using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class MainMenuButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI textMeshPro; // Directly reference the text component
    [SerializeField] private bool isMainMenu = false;
    private GameObject utilityLoader => GameObject.Find("UtilityLoader");
    private UtilityLoader utilityLoaderScript => utilityLoader.GetComponent<UtilityLoader>();
    private AudioSource hoverSound;
    [SerializeField] private AudioClip hoverClip;



    private void Start()
    {
        textMeshPro.color = new Color32(255, 255, 225, 100); // Dim color
        if (isMainMenu)
        {
            hoverSound = utilityLoaderScript.getGameObject("UISounds").GetComponent<AudioSource>();
        }

    }
    private void Awake()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponentInChildren<TextMeshProUGUI>(); // Auto-assign if not set in Inspecto
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

    public void OnClick()
    {
        if (isMainMenu)
        {
            hoverSound.clip = hoverClip;
            hoverSound.Play();
        }
    }
}
