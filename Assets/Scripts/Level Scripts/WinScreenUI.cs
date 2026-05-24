using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenUI : MonoBehaviour
{
    public static WinScreenUI Instance;

    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI coinsText;

    [Header("Animation")]
    [SerializeField] private Animator animator;

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

        Time.timeScale = 0f;
        /*
        coinsText.text =
            CoinManager.Instance.collectedCoins +
            " / " +
            CoinManager.Instance.totalCoins;
        */
        animator.SetTrigger("Win");
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;

        panel.SetActive(false);

        SceneManager.LoadScene(nextScene);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;

        panel.SetActive(false);

        SceneManager.LoadScene("MainMenu");
    }
}