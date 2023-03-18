using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        Time.timeScale = 1f;
        Destroy(GameObject.Find("NetworkManager"));
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }

    public void Exit() 
    { 
        Application.Quit();
    }
}
