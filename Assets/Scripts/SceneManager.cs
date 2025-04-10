using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using System.Collections;
using System.Collections.Generic;
using VisualNovel_2025;
using UnityEngine.SceneManagement;
using System.Linq;
public class SceneManager : MonoBehaviour
{
    public VariableStorageBehaviour yarnVariableStorage;
    [SerializeField] private Image backgroundImage; // Reference to the Image component
    [SerializeField] private Sprite[] backgroundSprites; // Array of background images
    [SerializeField] private GameObject CanvasWithFader; // Reference to the CanvasFader component

    [SerializeField] private Image[] characterImages;
    [SerializeField] private Sprite[] characterSprites;
    [SerializeField] private AudioSource[] audioObjects;
    [SerializeField] private GameObject[] audioSources;


    public Sprite GetBackgroundByName(string imageName)
    {
        return SearchArray(backgroundSprites, imageName);
    }

    private T SearchArray<T>(T[] array, string name) where T : Object
    {
        foreach (T element in array)
        {
            if (element.name == name)
            {
                return element;
            }
        }
        Debug.LogError("name " + array == null);
        Debug.LogError($"Element with name '{name}' not found in array!");
        return null;
    }

    [YarnCommand("change")]
    public void ChangeBackgroundImage(string imageName)
    {
        Sprite newImage = GetBackgroundByName(imageName);
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
        Sprite newImage = GetBackgroundByName(imageName);
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

    private IEnumerator FadeOutAndChangeImage(CanvasFader fader, Sprite newImage, float duration)
    {
        fader.FadeOut(duration);
        yield return new WaitForSeconds(duration);
        backgroundImage.sprite = newImage;
        fader.FadeIn(duration);
    }

    [YarnCommand("changecharacter")]
    public void ChangeCharacterImage(string characterObjectName, string imageName)

    {
        Image characterImage = SearchArray(characterImages, characterObjectName);
        if (characterImage != null)
        {
            Sprite newImage = SearchArray(characterSprites, imageName);
            if (newImage != null)
            {
                characterImage.enabled = true;
                characterImage.sprite = newImage;

            }
        }
    }

    [YarnCommand("changecharacterfade")]
    public void ChangeCharacterImageWithFade(string characterObjectName, string imageName, float fadeDuration)
    {
        Image characterImage = SearchArray(characterImages, characterObjectName);
        if (characterImage != null)
        {
            Sprite newImage = SearchArray(characterSprites, imageName);
            if (newImage != null)
            {
                CanvasFader fader = characterImage.GetComponent<CanvasFader>();

                if (fader != null)
                {
                    StartCoroutine(FadeOutAndChangeCharacterImage(fader, newImage, fadeDuration, characterImage));
                }
                else
                {
                    Debug.LogError("CanvasFader component is missing on the character image.");
                }
            }
        }
    }

    private IEnumerator FadeOutAndChangeCharacterImage(CanvasFader fader, Sprite newImage, float duration, Image characterImage)
    {
        fader.FadeOut(duration);
        yield return new WaitForSeconds(duration);
        characterImage.sprite = newImage;
        fader.FadeIn(duration);
    }


    [YarnCommand("movecharacter")]
    public void MoveCharacterImage(string characterName, float x, float y, float scaleX = 1f, float scaleY = 1f)
    {
        Image characterImage = SearchArray(characterImages, characterName);
        if (characterImage != null)
        {
            RectTransform rectTransform = characterImage.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(x, y);
            characterImage.GetComponent<RectTransform>().localScale = new Vector3(scaleX, scaleY, 1);
        }
    }

    [YarnCommand("showcharacter")]
    public void ShowCharacterImage(string characterName)
    {
        Image characterImage = SearchArray(characterImages, characterName);
        if (characterImage != null)
        {
            characterImage.enabled = true;
            characterImage.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    [YarnCommand("showcharacterfade")]
    public void ShowCharacterImageWithFade(string characterName, float fadeDuration)
    {
        Image characterImage = SearchArray(characterImages, characterName);
        if (characterImage != null)
        {
            CanvasFader fader = characterImage.GetComponent<CanvasFader>();
            characterImage.enabled = true;

            if (fader != null)
            {
                StartCoroutine(FadeInAndShowCharacterImage(fader, fadeDuration, characterImage));
            }
            else
            {
                Debug.LogError("CanvasFader component is missing on the character image.");
            }
        }
    }
    [YarnCommand("hidecharacter")]
    public void HideCharacterImage(string characterName)
    {
        Image characterImage = SearchArray(characterImages, characterName);
        if (characterImage != null)
        {
            characterImage.GetComponent<CanvasGroup>().alpha = 0;
            characterImage.enabled = false;
        }
    }


    [YarnCommand("hidecharacterfade")]
    public void HideCharacterImageWithFade(string characterName, float fadeDuration)
    {
        Image characterImage = SearchArray(characterImages, characterName);
        if (characterImage != null)
        {
            CanvasFader fader = characterImage.GetComponent<CanvasFader>();

            if (fader != null)
            {
                StartCoroutine(FadeOutAndHideCharacterImage(fader, fadeDuration, characterImage));
            }
            else
            {
                Debug.LogError("CanvasFader component is missing on the character image.");
            }
        }
    }

    private IEnumerator FadeOutAndHideCharacterImage(CanvasFader fader, float duration, Image characterImage)
    {
        fader.FadeOut(duration);
        yield return new WaitForSeconds(duration);
        characterImage.enabled = false;
    }

    private IEnumerator FadeInAndShowCharacterImage(CanvasFader fader, float duration, Image characterImage)
    {
        fader.FadeIn(duration);
        yield return new WaitForSeconds(duration);
        characterImage.enabled = true;
    }

    [YarnCommand("playaudio")]
    public void PlayAudio(string audioType, string audioName)
    {
        AudioSource audioSource = SearchArray(audioObjects, audioType);
        if (audioSource != null && audioSource.isPlaying == false)
        {
            audioSource.clip = SearchArray(audioSources, audioName).GetComponent<AudioSource>().clip;
            audioSource.Play();
        }
    }

    [YarnCommand("pauseaudio")]
    public void PauseAudio(string audioType)
    {
        AudioSource audioSource = SearchArray(audioObjects, audioType);
        if (audioSource != null)
        {
            audioSource.Pause();
        }
    }

    [YarnCommand("resumeaudio")]
    public void ResumeAudio(string audioType)
    {
        AudioSource audioSource = SearchArray(audioObjects, audioType);
        if (audioSource != null)
        {
            audioSource.UnPause();
        }
    }

    [YarnCommand("stopaudio")]
    public void StopAudio(string audioType)
    {
        AudioSource audioSource = SearchArray(audioObjects, audioType);
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    [YarnCommand("fadeaudio")]
    public void FadeAudio(string audioType, float targetVolume, float fadeDuration)
    {
        AudioSource audioSource = SearchArray(audioObjects, audioType);
        if (audioSource != null)
        {
            StartCoroutine(FadeAudioVolume(audioSource, targetVolume, fadeDuration));
        }
    }
    private IEnumerator FadeAudioVolume(AudioSource audioSource, float targetVolume, float duration, bool stopAfterFade = false)
    {
        float startVolume = audioSource.volume;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, t);
            yield return null;
        }
        audioSource.volume = targetVolume;
        if (stopAfterFade && targetVolume == 0)
        {
            audioSource.Stop();
        }
    }

    public string GetCurrentBackground()
    {
        // Debug.Log("Background: " + backgroundImage.sprite.name);
        // return backgroundImage != null ? backgroundImage.sprite.name : "";

        if (backgroundImage == null)
        {
            Debug.LogError("backgroundImage is NULL!");
            return "";
        }

        if (backgroundImage.sprite == null)
        {
            Debug.LogError("backgroundImage.sprite is NULL!");
            return "";
        }

        Debug.Log("Background: " + backgroundImage.sprite.name);
        return backgroundImage.sprite.name;
    }

    public string GetYarnVariable(string variableName)
    {
        if (yarnVariableStorage == null)
        {
            Debug.LogWarning("YarnVariableStorage is not assigned!");
            return null;
        }

        if (yarnVariableStorage.TryGetValue(variableName, out string stringValue))
        {
            return stringValue;
        }

        Debug.LogWarning($"Yarn variable {variableName} not found.");
        return null;
    }

}