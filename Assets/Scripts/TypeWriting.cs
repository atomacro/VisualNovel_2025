using UnityEngine;
using TMPro;
using System.Collections;

public class TypeWriting : MonoBehaviour
{
    public TMP_Text textComponent;
    public string textToWrite;
    public float timeBetweenCharacters = 0.1f;
    public bool destroyWhenComplete = false;


    public void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        textToWrite = textComponent.text;
        textComponent.text = "";
        StartTyping();
    }
    public void StartTyping()
    {
        StartCoroutine(ShowText());
    }

    public IEnumerator ShowText()
    {
        textComponent.text = "";
        foreach (char c in textToWrite)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(timeBetweenCharacters);
        }

        if (destroyWhenComplete)
        {
            Destroy(gameObject);
        }
    }
}
