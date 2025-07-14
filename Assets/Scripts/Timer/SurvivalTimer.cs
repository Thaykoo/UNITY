using UnityEngine;
using TMPro;  // Pour TextMeshProUGUI

public class SurvivalTimer : MonoBehaviour
{
    [Header("Référence UI")]
    [Tooltip("Le composant TextMeshProUGUI qui affiche le chrono en jeu")]
    public TextMeshProUGUI timerText;

    // Temps de départ et flag de course
    private float startTime;
    private bool running;

    // Temps final (en secondes) que le joueur a survécu
    public static float FinalTime { get; private set; }

    void Start()
    {
        // On démarre le chrono dès le début de la partie
        startTime = Time.time;
        running   = true;
    }

    void Update()
    {
        if (!running) 
            return;

        // Calcul du temps écoulé
        float t = Time.time - startTime;
        FinalTime = t;

        // Conversion minutes / secondes
        int minutes = (int)(t / 60);
        int seconds = (int)(t % 60);

        // Mise à jour de l’affichage
        timerText.text = $"Time: {minutes}:{seconds:00}";
    }

    void OnEnable()
    {
        // Abonnement à l’événement de mort
        Health.OnDeath += StopTimer;
    }

    void OnDisable()
    {
        // Désabonnement
        Health.OnDeath -= StopTimer;
    }

    private void StopTimer()
    {
        // Stoppe le chrono
        running = false;

        // Déclenche l’affichage du Game Over
        GameOverManager.Instance.ShowGameOver();
    }
}

