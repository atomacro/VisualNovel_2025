using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class AutoGameBarFunctionality : MonoBehaviour
{
    [SerializeField] private GameObject lineViewObject;
    private LineView lineView;
    private bool isAuto = false;

    private void Start()
    {
        lineView = lineViewObject.GetComponent<LineView>();
        lineView.autoAdvance = isAuto;
        gameObject.GetComponent<Button>().onClick.AddListener(ToggleAuto);
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = isAuto ? "Auto" : "Manual";
    }

    private void ToggleAuto()
    {
        isAuto = !isAuto;
        lineView.autoAdvance = isAuto;
        lineView.holdTime = isAuto ? 2f : 0f;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = isAuto ? "Auto" : "Manual";
    }
}
