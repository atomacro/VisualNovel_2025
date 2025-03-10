using UnityEngine;

public class MainMenuFade : MonoBehaviour
{
    void Start()
    {
     gameObject.GetComponent<CanvasFader>().FadeIn(1f);
    }
}
