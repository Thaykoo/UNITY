using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats de progression")]
    public int level = 1;
    public int currentXP = 0;
    public int xpToLevelUp = 10;

    // Événement pour la UI
    public static event Action<int, int> OnXPChanged;
    public static event Action<int> OnLevelUp;

    void Start()
    {
        // Initialise la barre d’XP
        OnXPChanged?.Invoke(currentXP, xpToLevelUp);
    }

    public void GainXP(int amount)
    {
        currentXP += amount;
        // Notifie le HUD
        OnXPChanged?.Invoke(currentXP, xpToLevelUp);

        // Check level up
        if (currentXP >= xpToLevelUp)
        {
            currentXP -= xpToLevelUp;
            level++;
            // Augmente le palier pour le prochain niveau (exponentiel ou fixe)
            xpToLevelUp += 5;  
            OnLevelUp?.Invoke(level);
            // On notifie aussi la barre d’XP remise à zéro
            OnXPChanged?.Invoke(currentXP, xpToLevelUp);
        }
    }
}

