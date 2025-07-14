using UnityEngine;
using UnityEngine.UI;
using TMPro;    // nécessaire pour TMP_Text
using System;

public class HUDController : MonoBehaviour
{
    [Header("UI References")]
    public Slider healthBar;     // ton Slider de vie
    public Slider xpBar;         // ton Slider d’XP
    public TMP_Text levelText;   // ton TextMeshProUGUI pour afficher "Lvl X"

    private PlayerStats stats;

    void Awake()
    {
        // Trouve le PlayerStats dans la scène pour récupérer les valeurs initiales
        stats = FindObjectOfType<PlayerStats>();
    }

    void Start()
    {
        // Initialise tout de suite l’affichage
        if (stats != null)
        {
            // Texte de niveau initial
            if (levelText != null)
                levelText.text = $"Lvl {stats.level}";
            // Barre d’XP initiale
            if (xpBar != null)
            {
                xpBar.maxValue = stats.xpToLevelUp;
                xpBar.value    = stats.currentXP;
            }
        }
    }

    void OnEnable()
    {
        Health.OnHealthChanged    += UpdateHealthBar;
        PlayerStats.OnXPChanged   += UpdateXPBar;
        PlayerStats.OnLevelUp     += UpdateLevelText;
    }

    void OnDisable()
    {
        Health.OnHealthChanged    -= UpdateHealthBar;
        PlayerStats.OnXPChanged   -= UpdateXPBar;
        PlayerStats.OnLevelUp     -= UpdateLevelText;
    }

    /// <summary>
    /// Valeurs de vie reçues : met à jour la barre (0→1).
    /// </summary>
    void UpdateHealthBar(int currentHP, int maxHP)
    {
        if (healthBar != null)
            healthBar.value = currentHP / (float)maxHP;
    }

    /// <summary>
    /// Valeurs d’XP reçues : met à jour la barre (0→1).
    /// </summary>
    void UpdateXPBar(int currentXP, int xpToLevelUp)
    {
        if (xpBar != null)
        {
            xpBar.maxValue = xpToLevelUp;
            xpBar.value    = currentXP;
        }
    }

    /// <summary>
    /// Callback appelé à chaque passage de niveau.
    /// </summary>
    void UpdateLevelText(int newLevel)
    {
        if (levelText != null)
            levelText.text = $"Lvl {newLevel}";
    }
}

