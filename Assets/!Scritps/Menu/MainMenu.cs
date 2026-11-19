using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string SceneName = "MainMenu";

    public void play()
    {
        SceneManager.LoadScene(SceneName);
    }
}
