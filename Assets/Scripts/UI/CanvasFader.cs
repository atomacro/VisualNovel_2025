using System.Collections;
using UnityEngine;

public class CanvasFader : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FadeIn(float duration)
    {
        StartCoroutine(FadeCanvas(0f, 1f, duration));
    }

    public float currentAlpha;
    public void StopFade()
    {
        canvasGroup.alpha = currentAlpha;
        StopAllCoroutines();
    }

    public void FadeOut(float duration)
    {
        StartCoroutine(FadeCanvas(1f, 0f, duration));
    }

    private IEnumerator FadeCanvas(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            currentAlpha = canvasGroup.alpha;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }
}
