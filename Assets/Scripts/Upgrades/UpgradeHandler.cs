using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{
    UpgradeSO chosen;
    float lastUse;

    public void Apply(UpgradeSO upg)
    {
        chosen = upg;
        lastUse = Time.time - upg.cooldown; // prêt à tirer
    }

    void Update()
    {
        if (chosen == null) return;
        if (Time.time >= lastUse + chosen.cooldown)
        {
            lastUse = Time.time;
            Use(chosen);
        }
    }

    void Use(UpgradeSO upg)
    {
        switch(upg.upgradeName)
        {
            case "Fireball":
                // instancier une boule, target automatique…
                break;
            case "LifeDrain":
                // OverlapCircleAll + dégâts + heal…
                break;
            case "Shot":
                // Raycast devant + dégâts
                break;
        }
    }
}

