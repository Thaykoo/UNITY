using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Button retryButton;
    public Button menuButton;

    void OnEnable()  => Health.OnDeath += ShowGameOver;
    void OnDisable() => Health.OnDeath -= ShowGameOver;

    void Start()
    {
        gameOverPanel.SetActive(false);
        retryButton.onClick.AddListener(RestartGame);
        menuButton.onClick.AddListener(ReturnToMenu);
    }

    void ShowGameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GamePlay");
    }

    void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

