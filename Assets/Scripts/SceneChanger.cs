using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonLoader : MonoBehaviour
{
    public string loadingSceneName = "LoadingScene";

    public void LoadGame()
    {
        SceneManager.LoadScene(loadingSceneName);
    }
}
