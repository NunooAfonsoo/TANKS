using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }

    public void Exit() 
    { 
        Application.Quit();
    }
}