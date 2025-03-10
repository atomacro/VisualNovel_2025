using UnityEngine;
using UnityEngine.UI;
public class LogGameBarNavigation : MonoBehaviour
{

    [SerializeField] private GameObject DialogueLog;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => DialogueLog.SetActive(true));
    }


}
