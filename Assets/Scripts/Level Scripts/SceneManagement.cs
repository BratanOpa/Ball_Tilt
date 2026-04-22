using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private static bool uiLoaded = false; // håller koll globalt

    void Awake()
    {
        if (!uiLoaded)
        {
            Debug.Log("Loading UI Scene (first time only)");
            SceneManager.LoadScene("UI", LoadSceneMode.Additive);
            uiLoaded = true;
        }
        else
        {
            Debug.Log("UI already loaded, skipping");
        }


    }
}