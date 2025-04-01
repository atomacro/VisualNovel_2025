using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using VisualNovel_2025;
using UnityEngine.SceneManagement;

public class MainMenuButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI textMeshPro; // Directly reference the text component
    [SerializeField] private bool isMainMenu = false;
    private AudioSource hoverSound;
    [SerializeField] private AudioClip hoverClip;
    private HelperClass helper;

    private Scene Utilities;




    private void Start()
    {
        Utilities = UnityEngine.SceneManagement.SceneManager.GetSceneByName("Utilities");
        helper = new HelperClass();
        textMeshPro.color = new Color32(255, 255, 225, 100); // Dim color
        if (isMainMenu)
        {
            hoverSound = helper.GetGameObjectFromAnotherScene("UISounds", Utilities).GetComponent<AudioSource>();
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
