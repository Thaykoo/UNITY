using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class Health : MonoBehaviour
{
    [Header("Paramètres de vie")]
    public int maxHP = 100;
    public int currentHP { get; private set; }

    // Événements pour notifier la UI ou d'autres systèmes
    public static event Action<int, int> OnHealthChanged;
    public static event Action OnDeath;

    void Awake()
    {
        currentHP = maxHP;
        // Notifie la valeur initiale de vie
        OnHealthChanged?.Invoke(currentHP, maxHP);
    }

    /// <summary> Inflige des dégâts à cet objet. </summary>
    public void TakeDamage(int amount)
    {
        currentHP = Mathf.Max(currentHP - amount, 0);
        OnHealthChanged?.Invoke(currentHP, maxHP);

        if (currentHP == 0)
            Die();
    }

    /// <summary> Rend de la vie à cet objet. </summary>
    public void Heal(int amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
        OnHealthChanged?.Invoke(currentHP, maxHP);
    }

    /// <summary> Gère la mort : déclenche OnDeath puis désactive le GameObject. </summary>
    void Die()
    {
        Debug.Log("[Health] Die() called");   // Log pour vérifier l'appel
        OnDeath?.Invoke();
        gameObject.SetActive(false);
    }
}

