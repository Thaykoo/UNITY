using UnityEngine;
using TMPro;  // ou UnityEngine.UI si tu utilises UI.Text instead

public class SurvivalTimer : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI timerText;   // ou public Text timerText;

    float startTime;
    bool running = false;

    void OnEnable()
    {
        // Quand le joueur meurt, on arrête le timer
        Health.OnDeath += StopTimer;
    }

    void OnDisable()
    {
        Health.OnDeath -= StopTimer;
    }

    void Start()
    {
        StartTimer();
    }

    void Update()
    {
        if (!running) return;
        float t = Time.time - startTime;
        int minutes = (int)(t / 60);
        int seconds = (int)(t % 60);
        timerText.text = $"Time: {minutes}:{seconds:00}";
    }

    public void StartTimer()
    {
        startTime = Time.time;
        running = true;
    }

    public void StopTimer()
    {
        running = false;
    }
}

