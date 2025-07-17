using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats de progression")]
    public int level = 1;
    public int currentXP = 0;
    public int xpToLevelUp = 10;

    public static event Action<int, int> OnXPChanged;
    public static event Action<int>       OnLevelUp;

    void Start()
    {
        OnXPChanged?.Invoke(currentXP, xpToLevelUp);
    }

    public void GainXP(int amount)
    {
        currentXP += amount;
        OnXPChanged?.Invoke(currentXP, xpToLevelUp);

        if (currentXP >= xpToLevelUp)
        {
            currentXP -= xpToLevelUp;
            level++;
            xpToLevelUp += 5;
            OnLevelUp?.Invoke(level);
            OnXPChanged?.Invoke(currentXP, xpToLevelUp);
        }
    }
}

