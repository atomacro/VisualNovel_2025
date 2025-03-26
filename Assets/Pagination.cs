using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Pagination : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pageNumberText;
    private int pageNumber = 1;
    private int firstDisplayed = 0;
    private int lastDisplayed = 5;
    private int maxPage = 3;
    [SerializeField] private Sprite[] sampleImages;
    [SerializeField] GameObject Row1;
    [SerializeField] GameObject Row2;
    private List<Image> images = new List<Image>();
    private void Start()
    {
        images.AddRange(Row1.GetComponentsInChildren<Image>());
        images.AddRange(Row2.GetComponentsInChildren<Image>());
        maxPage = sampleImages.Length / 6;
        maxPage += sampleImages.Length % 6 == 0 ? 0 : 1;
        LoadImages(0, 5);
    }
    public void Increment()
    {
        if (pageNumber == maxPage) return;
        pageNumber++;
        firstDisplayed += 6;
        lastDisplayed += 6;
        LoadPage();
    }

    public void Decrement()
    {
        if (pageNumber == 1) return;
        pageNumber--;
        firstDisplayed -= 6;
        lastDisplayed -= 6;
        LoadPage();
    }

    private void LoadPage()
    {
        pageNumberText.text = pageNumber.ToString();
        Debug.Log(pageNumber + " " + firstDisplayed + " " + lastDisplayed);
        LoadImages(firstDisplayed, lastDisplayed);
    }

    private void LoadImages(int start, int end)
    {
        int imageCounter = start;
        for (int i = 0; i < 6; i++, imageCounter++)
        {
            if (imageCounter >= start && imageCounter <= end && imageCounter < sampleImages.Length)
            {
                images[i].sprite = sampleImages[imageCounter];
                images[i].gameObject.SetActive(true);
            }
            else
            {
                images[i].gameObject.SetActive(false);
            }
        }
    }
}

