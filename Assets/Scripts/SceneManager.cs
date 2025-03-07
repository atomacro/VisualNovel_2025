using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using System.Collections;


public class SceneManager : MonoBehaviour
{

    public void ChangeCanvas(string currentCanvasName, string nextCanvasName)
    {
        GameObject currentCanvas = GameObject.Find(currentCanvasName);
        GameObject nextCanvas = GameObject.Find(nextCanvasName);

        if (currentCanvas != null && nextCanvas != null)
        {
            currentCanvas.SetActive(false);
            nextCanvas.SetActive(true);
        }
        else
        {
            Debug.LogError("Canvas not found! Check your object names.");
        }
    }

    [YarnCommand("change")]
    public void YarnChangeCanvas(string currentCanvasName, string nextCanvasName)
    {
        ChangeCanvas(currentCanvasName, nextCanvasName);
    }

    [YarnCommand("changefade")]
    public void ChangeCanvasWithFade(string currentCanvasName, string nextCanvasName, float fadeDuration)
    {
        GameObject currentCanvas = GameObject.Find(currentCanvasName);
        GameObject nextCanvas = GameObject.Find(nextCanvasName);

        if (currentCanvas != null && nextCanvas != null)
        {
            CanvasFader currentFader = currentCanvas.GetComponent<CanvasFader>();
            CanvasFader nextFader = nextCanvas.GetComponent<CanvasFader>();

            if (currentFader != null && nextFader != null)
            {
                StartCoroutine(FadeOutFirst(currentFader, nextFader, fadeDuration));
            }
            else
            {
                Debug.LogError("CanvasFader component is missing on one of the canvases.");
            }
        }
        else
        {
            Debug.LogError("Canvas not found! Check your object names.");
        }
    }

    private IEnumerator FadeOutFirst(CanvasFader currentFader, CanvasFader nextFader, float duration)
    {
        nextFader.gameObject.SetActive(true);
        currentFader.FadeOut(duration);
        yield return new WaitForSeconds(duration);
        currentFader.gameObject.SetActive(false);
    }
}
