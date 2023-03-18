using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private void Awake()
    {
        BackgroundMusic[] backgroundMusicObjects = FindObjectsOfType<BackgroundMusic>();
        foreach (var backgroundMusic in backgroundMusicObjects)
        {
            if(backgroundMusic != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(this);
    }
}