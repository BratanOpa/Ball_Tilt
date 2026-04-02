using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] string sceneName; // Namnet på scenen som ska laddas

    public void LoadScene()
    {
        // Laddar scenen med namnet som du skrivit in i Inspector
        SceneManager.LoadScene(sceneName);
    }
}