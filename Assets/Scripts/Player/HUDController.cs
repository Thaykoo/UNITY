using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HUDController : MonoBehaviour
{
    [Header("UI References")]
    public Slider healthBar;
    public Slider xpBar;
    public TMP_Text levelText;

    private PlayerStats stats;

    void Awake()
    {
        stats = FindObjectOfType<PlayerStats>();
    }

    void Start()
    {
        if (stats != null)
        {
            if (levelText != null)
                levelText.text = $"Lvl {stats.level}";
            if (xpBar != null)
            {
                xpBar.maxValue = stats.xpToLevelUp;
                xpBar.value    = stats.currentXP;
            }
        }
    }

    void OnEnable()
    {
        Health.OnHealthChanged  += UpdateHealthBar;
        PlayerStats.OnXPChanged += UpdateXPBar;
        PlayerStats.OnLevelUp   += UpdateLevelText;
    }

    void OnDisable()
    {
        Health.OnHealthChanged  -= UpdateHealthBar;
        PlayerStats.OnXPChanged -= UpdateXPBar;
        PlayerStats.OnLevelUp   -= UpdateLevelText;
    }

    void UpdateHealthBar(int currentHP, int maxHP)
    {
        if (healthBar != null)
            healthBar.value = currentHP / (float)maxHP;
    }

    void UpdateXPBar(int currentXP, int xpToLevelUp)
    {
        if (xpBar != null)
        {
            xpBar.maxValue = xpToLevelUp;
            xpBar.value    = currentXP;
        }
    }

    void UpdateLevelText(int newLevel)
    {
        if (levelText != null)
            levelText.text = $"Lvl {newLevel}";
    }
}

