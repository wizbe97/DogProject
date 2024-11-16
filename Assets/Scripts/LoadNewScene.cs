using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    public string sceneName;
    public GameManagerSO gameManager;

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            gameManager.saveManager.SaveAllData();
            SceneManager.LoadScene(sceneName);
        }

    }
}