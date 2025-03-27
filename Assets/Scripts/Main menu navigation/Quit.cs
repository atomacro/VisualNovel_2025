using UnityEngine;

public class Quit : MonoBehaviour
{
    [SerializeField] private GameObject confirmationModalPrefab;
    public void onQuit()
    {
        GameObject modalInstance = Instantiate(confirmationModalPrefab);
        ConfirmationModal confirmationModal = modalInstance.GetComponent<ConfirmationModal>();
        confirmationModal.SetWarningMessage("Are you sure you want to quit?");
        confirmationModal.OnConfirmAction.AddListener(QuitGame);
        confirmationModal.OnCancelAction.AddListener(confirmationModal.DestroyConfimationModal);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
    }
}
