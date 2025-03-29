using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hover : MonoBehaviour
{
    [SerializeField] private GameObject background; 
    [SerializeField] private GameObject text; 

    private Image backgroundImage;
    private TextMeshProUGUI textMeshPro;

    private void Start(){
        backgroundImage = background.GetComponent<Image>();
        textMeshPro = text.GetComponent<TextMeshProUGUI>();
    }

    public void OnMouseEnter()
    {
        backgroundImage.color = new Color32(0, 0, 0, 30);
        textMeshPro.color = new Color32(255, 255, 255, 255);
    }

    public void OnMouseExit()
    {
        backgroundImage.color = new Color32(0, 0, 0, 20);
        textMeshPro.color = new Color32(255, 255, 255, 100);
    }
}