using UnityEngine;
using System;

public class PlayerExperience : MonoBehaviour
{
    [Header("Progression XP")]
    public int level = 1;
    public float xp = 0f;
    public float xpToNext = 100f;

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
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("[Test XP] Touche L détectée, j’ajoute 999 XP.");
            AddXP(999f);
        }
    }

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

