using UnityEngine;
using UnityEngine.UI;

public class ReusableBackScript : MonoBehaviour
{
    [SerializeField] private GameObject ObjectToClose;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(()=>ObjectToClose.SetActive(false));
    }
}
