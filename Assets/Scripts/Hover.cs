using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using VisualNovel_2025;

public class Hover : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject text;
    private HelperClass helper = new HelperClass();
    private Scene Utilities;

    private Image backgroundImage;
    private TextMeshProUGUI textMeshPro;
    private AudioSource UISounds;
    [SerializeField] private AudioClip hoverClip;
    [SerializeField] private AudioClip clickClip;    

    private void Start()
    {
        backgroundImage = background.GetComponent<Image>();
        textMeshPro = text.GetComponent<TextMeshProUGUI>();
        UISounds = helper.GetGameObjectFromAnotherScene("UISounds", Utilities).GetComponent<AudioSource>();

    }

    public void OnMouseEnter()
    {
        backgroundImage.color = new Color32(0, 0, 0, 30);
        textMeshPro.color = new Color32(255, 255, 255, 255);
        UISounds.clip = hoverClip;
        UISounds.Play();
    }
    public void OnClick(){
        UISounds.clip = clickClip;
        UISounds.Play();
    }

    public void OnMouseExit()
    {
        backgroundImage.color = new Color32(0, 0, 0, 20);
        textMeshPro.color = new Color32(255, 255, 255, 100);
    }
}