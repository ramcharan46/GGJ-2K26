using UnityEngine;

public class LoadSceneButton : MonoBehaviour
{
    public string sceneName;

    public void LoadScene()
    {
        SceneLoader.LoadScene(sceneName);
    }
}
