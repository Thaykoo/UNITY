using UnityEngine;
using UnityEngine.UI;
using System;

public class HUDController : MonoBehaviour
{
    [Header("UI References")]
    public Slider healthBar;
    public Slider xpBar;

    void OnEnable()
    {
        Health.OnHealthChanged += UpdateHealthBar;
        PlayerStats.OnXPChanged   += UpdateXPBar;
    }

    void OnDisable()
    {
        Health.OnHealthChanged -= UpdateHealthBar;
        PlayerStats.OnXPChanged   -= UpdateXPBar;
    }

    /// <summary>
    /// Met à jour la barre de vie (valeur entre 0 et 1).
    /// </summary>
    void UpdateHealthBar(int currentHP, int maxHP)
    {
        if (healthBar != null)
            healthBar.value = currentHP / (float)maxHP;
    }

    /// <summary>
    /// Met à jour la barre d’XP (valeur entre 0 et 1).
    /// </summary>
    void UpdateXPBar(int currentXP, int xpToLevelUp)
    {
        if (xpBar != null)
            xpBar.value = currentXP / (float)xpToLevelUp;
    }
}

