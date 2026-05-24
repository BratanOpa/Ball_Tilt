using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public int collectedCoins;
    public int totalCoins;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void CollectCoin()
    {
        collectedCoins++;
    }

    public void ResetCoins()
    {
        collectedCoins = 0;
        totalCoins = 0;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetCoins();
    }
}