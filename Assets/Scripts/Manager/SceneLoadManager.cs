using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private static SceneLoadManager instance = null;

    public static SceneLoadManager Instance => instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
