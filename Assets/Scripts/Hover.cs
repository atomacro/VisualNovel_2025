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
        backgroundImage.color = new Color32(255, 255, 225, 5);
        textMeshPro.color = new Color32(255, 255, 225, 255);
    }

    public void OnMouseExit()
    {
        backgroundImage.color = new Color32(255, 255, 225, 1);
        textMeshPro.color = new Color32(255, 255, 225, 40);
    }
}