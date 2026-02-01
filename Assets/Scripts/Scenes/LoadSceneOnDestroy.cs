using UnityEngine;

public class LoadSceneOnDestroy : MonoBehaviour
{
    [Tooltip("Scene to load AFTER this object is destroyed")]
    public string sceneToLoad = "GameScene";

    [Tooltip("Delay before loading scene")]
    public float delay = 0f;

    private void OnDestroy()
    {
        // Prevent errors when quitting play mode
        if (!Application.isPlaying) return;

        if (delay > 0)
        {
            // Start delayed load using a helper
            SceneLoadHelper.Instance.LoadAfterDelay(sceneToLoad, delay);
        }
        else
        {
            SceneLoader.LoadScene(sceneToLoad);
        }
    }
}
