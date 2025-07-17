using UnityEngine;
using System;

public class PlayerExperience : MonoBehaviour
{
    [Header("Progression XP")]
    public int level = 1;
    public float xp = 0f;
    public float xpToNext = 100f;

    // Événement déclenché au passage de niveau
    public static event Action OnLevelUp;

    void Awake()
    {
        Debug.Log("[PlayerExperience] Awake – script chargé et GameObject actif.");
    }

    void OnEnable()
    {
        Debug.Log("[PlayerExperience] OnEnable – composant activé.");
    }

    void Start()
    {
        Debug.Log($"[PlayerExperience] Start – level={level}, xp={xp}/{xpToNext}");
    }

    void Update()
    {
        // Test manuel : appuie sur L pour déclencher AddXP et voir les logs
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("[Test XP] Touche L détectée, j’ajoute 999 XP.");
            AddXP(999f);
        }
    }

    /// <summary>
    /// Ajoute de l'XP et déclenche OnLevelUp si on passe de lvl 1 à 2.
    /// </summary>
    public void AddXP(float amount)
    {
        Debug.Log($"[PlayerExperience] AddXP({amount}) appelé.");
        xp += amount;
        Debug.Log($"[PlayerExperience] xp → {xp} / seuil={xpToNext}");

        if (level == 1 && xp >= xpToNext)
        {
            level = 2;
            Debug.Log("[PlayerExperience] Passage niveau 2 ! OnLevelUp va être invoqué.");
            OnLevelUp?.Invoke();
        }
    }

    void OnDisable()
    {
        Debug.Log("[PlayerExperience] OnDisable – composant désactivé.");
    }
}

