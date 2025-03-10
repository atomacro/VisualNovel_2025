using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using System.Collections;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private Image backgroundImage; // Reference to the Image component
    [SerializeField] private Sprite[] backgroundSprites; // Array of background images
    [SerializeField] private GameObject CanvasWithFader; // Reference to the CanvasFader component

    private Sprite GetSpriteByName(string imageName)
    {
        foreach (Sprite sprite in backgroundSprites)
        {
            if (sprite.name == imageName)
            {
                return sprite;
            }
        }
        Debug.LogError($"Sprite with name '{imageName}' not found!");
        return null;
    }

    [YarnCommand("change")]
    public void ChangeBackgroundImage(string imageName)
    {
        Sprite newImage = GetSpriteByName(imageName);
        if (newImage != null && backgroundImage != null)
        {
            backgroundImage.sprite = newImage;
        }
        else
        {
            Debug.LogError("Background Image component or sprite not found!");
        }
    }

    [YarnCommand("changefade")]
    public void ChangeBackgroundImageWithFade(string imageName, float fadeDuration)
    {
        Sprite newImage = GetSpriteByName(imageName);
        if (newImage != null && backgroundImage != null && CanvasWithFader != null)
        {
            CanvasFader fader = CanvasWithFader.GetComponent<CanvasFader>();

            if (fader != null)
            {
                StartCoroutine(FadeOutAndChangeImage(fader, newImage, fadeDuration));
            }
            else
            {
                Debug.LogError("CanvasFader component is missing on the background image.");
            }
        }
        else
        {
            Debug.LogError("Background Image component or sprite not found!");
        }
    }

    public IEnumerator FadeOutAndChangeImage(CanvasFader fader, Sprite newImage, float duration)
    {
        // Fade out the current image
        fader.FadeOut(duration);
        yield return new WaitForSeconds(duration);

        // Change the image
        backgroundImage.sprite = newImage;

        // Fade in the new image
        fader.FadeIn(duration);
    }
}
