using UnityEngine;
using TMPro;

public class SurvivalTimer : MonoBehaviour
{
    [Header("Référence UI")]
    [Tooltip("Le composant TextMeshProUGUI qui affiche le chrono en jeu")]
    public TextMeshProUGUI timerText;

    private float startTime;
    private bool running;

    public static float FinalTime { get; private set; }

    void Start()
    {
        startTime = Time.time;
        running   = true;
    }

    void Update()
    {
        if (!running) return;

        float t = Time.time - startTime;
        FinalTime = t;

        int minutes = (int)(t / 60);
        int seconds = (int)(t % 60);

        timerText.text = $"Time: {minutes}:{seconds:00}";
    }

    void OnEnable()
    {
        Health.OnDeath += StopTimer;
    }

    void OnDisable()
    {
        Health.OnDeath -= StopTimer;
    }

    private void StopTimer()
    {
        running = false;
        GameOverManager.Instance.ShowGameOver();
    }
}

