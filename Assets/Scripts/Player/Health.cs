using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class Health : MonoBehaviour
{
    [Header("Paramètres de vie")]
    public int maxHP = 100;
    public int currentHP { get; private set; }

    public static event Action<int, int> OnHealthChanged;
    public static event Action OnDeath;

    void Awake()
    {
        currentHP = maxHP;
        OnHealthChanged?.Invoke(currentHP, maxHP);
    }

    public void TakeDamage(int amount)
    {
        currentHP = Mathf.Max(currentHP - amount, 0);
        OnHealthChanged?.Invoke(currentHP, maxHP);
        if (currentHP == 0)
            Die();
    }

    public void Heal(int amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
        OnHealthChanged?.Invoke(currentHP, maxHP);
    }

    void Die()
    {
        Debug.Log("[Health] Die() called");
        OnDeath?.Invoke();
        gameObject.SetActive(false);
    }
}

