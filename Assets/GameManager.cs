using System.Collections;
using UnityEngine;
using Yarn;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject lineViewObj;
    [SerializeField] private CanvasFader canvasFader;
    LineView lineView;

    private void Start()
    {
        lineView = lineViewObj.gameObject.GetComponent<LineView>();
        lineView.typewriterEffectSpeed = PlayerPrefs.GetFloat("TextSpeed", 50);
        canvasFader.FadeIn(1.5f);
    }

    private IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(1.5f);
    }


}
