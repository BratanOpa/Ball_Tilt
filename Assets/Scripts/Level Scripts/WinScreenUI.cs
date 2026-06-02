using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenUI : MonoBehaviour
{
    public static WinScreenUI Instance;

    
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI coinsText;

    
    [SerializeField] private ParticleSystem winParticles;

    
    [SerializeField] private AudioClip winSound;
    private AudioSource[] playerAudioSources;

    private string nextScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        panel.SetActive(false);
    }

    public void Show(string nextSceneName)
    {
        nextScene = nextSceneName;

        panel.SetActive(true);

        GameSettings.freezeScreenActive = true;


        Time.timeScale = 0f;

        coinsText.text = "Coins collected: " + CoinManager.Instance.collectedCoins + " / " + CoinManager.Instance.totalCoins;
        AudioManager.Instance.PlaySFX(winSound);

        // Start particles
        if (winParticles != null)
        {
            winParticles.Play();
        }


    }

    public void NextLevel()
    {
        Time.timeScale = 1f;

        // Stop particles
        if (winParticles != null)
        {
            winParticles.Stop();
            winParticles.Clear();
        }


        panel.SetActive(false);

        GameSettings.freezeScreenActive = false;

        SceneManager.LoadScene(nextScene);
        CoinManager.Instance.ResetCoins();

    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;


        // Stop particles
        if (winParticles != null)
        {
            winParticles.Stop();
            winParticles.Clear();
        }

        panel.SetActive(false);

        GameSettings.freezeScreenActive = false;

        CoinManager.Instance.ResetCoins();
        SceneManager.LoadScene("mainMenu");

    }
}