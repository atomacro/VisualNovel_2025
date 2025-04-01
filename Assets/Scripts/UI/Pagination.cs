using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Yarn.Unity;
using System.Linq;
using VisualNovel_2025;
using UnityEngine.SceneManagement;

public class Pagination : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pageNumberText;
    private int pageNumber = 1;
    private int firstDisplayed = 0;
    private int lastDisplayed = 5;
    private int maxPage = 3;
    [SerializeField] private List<Dictionary<Sprite, bool>> Images = new List<Dictionary<Sprite, bool>>();
    [SerializeField] GameObject Row1;
    [SerializeField] GameObject Row2;
    [SerializeField] Sprite[] backgroundSprites;
    private List<Image> images = new List<Image>();
    private GameObject _backgrounds;
    private Backgrounds backgrounds;

    private bool isImagesLoaded = false;

    private void Awake()
    {
        LoadImages();
    }

    private void LoadImages()
    {
        if (backgroundSprites == null || backgroundSprites.Length == 0)
        {
            if (isImagesLoaded) return;
            _backgrounds = GameObject.Find("Backgrounds");
            backgrounds = _backgrounds.GetComponent<Backgrounds>();
            backgroundSprites = backgrounds.backgroundSprites.ToArray();
            if (_backgrounds == null || backgrounds == null) Debug.LogError("Backgrounds not found!");
            foreach (Sprite bg in backgroundSprites)
            {
                if (bg.name == "Black") continue;
                Images.Add(new Dictionary<Sprite, bool> { { bg, false } });
            }
            isImagesLoaded = true;
        }
    }
    private void Start()
    {
        images.AddRange(Row1.GetComponentsInChildren<Image>());
        images.AddRange(Row2.GetComponentsInChildren<Image>());
        maxPage = Images.Count / 6;
        maxPage += Images.Count % 6 == 0 ? 0 : 1;
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

        for (int i = 0; i < images.Count; i++, imageCounter++)
        {
            if (imageCounter >= start && imageCounter <= end && imageCounter < Images.Count)
            {
                foreach (var spriteEntry in Images[imageCounter])
                {
                    Debug.Log($"Loading image {spriteEntry.Key.name} with value {spriteEntry.Value}");
                    images[i].sprite = spriteEntry.Key;
                    images[i].color = spriteEntry.Value ? new Color32 (255, 255, 255, 255) : new Color32 (20, 20, 20, 255);
                    images[i].gameObject.SetActive(true);
                    break;
                }
            }
            else
            {
                images[i].gameObject.SetActive(false);
            }
        }
    }
    public void SetBackgroundValueTrue(string name)
    {
        LoadImages();

        foreach (var bgDict in Images)
        {
            foreach (var key in bgDict.Keys.ToList())
            {
                if (key.name == name)
                {
                    bgDict[key] = true;
                    Debug.Log($"Background {name} set to true!");
                    return;
                }
            }
        }

        Debug.LogWarning($"Sprite {name} not found in Images list!");
    }
}

