using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{
    UpgradeSO chosen;
    float lastUse;

    public void Apply(UpgradeSO upg)
    {
        chosen = upg;
        lastUse = Time.time - upg.cooldown;
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
        switch (upg.upgradeName)
        {
            case "Fireball":
                break;
            case "LifeDrain":
                break;
            case "Shot":
                break;
        }
    }
}

