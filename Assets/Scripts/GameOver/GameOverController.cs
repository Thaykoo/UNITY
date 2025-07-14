using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    [Header("UI Game Over")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public Button retryButton;
    public Button menuButton;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void OnEnable()
    {
        Health.OnDeath += ShowGameOver;
    }

    void OnDisable()
    {
        Health.OnDeath -= ShowGameOver;
    }

    void Start()
    {
        gameOverPanel.SetActive(false);
        retryButton.onClick.AddListener(OnRetry);
        menuButton.onClick.AddListener(OnMenu);
    }

    public void ShowGameOver()
    {
        Debug.Log("💀 ShowGameOver() appelé");  // pour vérifier qu'on y arrive
        Time.timeScale = 0f;

        float t = SurvivalTimer.FinalTime;
        int m = (int)(t / 60);
        int s = (int)(t % 60);
        gameOverText.text = $"You survived: {m}:{s:00}";

        gameOverPanel.SetActive(true);
    }

    void OnRetry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GamePlay");
    }

    void OnMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

