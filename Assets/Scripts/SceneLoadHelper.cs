using UnityEngine;
using System.Collections;

public class SceneLoadHelper : MonoBehaviour
{
    public static SceneLoadHelper Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadAfterDelay(string sceneName, float delay)
    {
        StartCoroutine(LoadRoutine(sceneName, delay));
    }

    IEnumerator LoadRoutine(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneLoader.LoadScene(sceneName);
    }
}
