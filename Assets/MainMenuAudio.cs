using UnityEngine;
using UnityEngine.SceneManagement;
using VisualNovel_2025;

public class MainMenuAudio : MonoBehaviour
{

    [SerializeField] private AudioClip audioClip;
    private HelperClass helper = new HelperClass();
    private Scene Utilities => UnityEngine.SceneManagement.SceneManager.GetSceneByName("Utilities");
    public void Start(){

        AudioSource audioSource = helper.GetGameObjectFromAnotherScene("UISounds", Utilities).GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
