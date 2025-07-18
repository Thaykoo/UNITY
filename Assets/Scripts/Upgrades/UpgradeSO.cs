using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/New Upgrade")]
public class UpgradeSO : ScriptableObject
{
    [Header("Affichage")]
    public string upgradeName;
    public Sprite icon;

    [Header("Paramètres de base")]
    public UpgradeType type;
    public int baseDamage = 20;
    public float cooldown = 1f;
    public float duration;

    [Header("Fireball only")]
    public float projectileSpeed = 15f;

    [Header("LifeDrain & Shot")]
    public float radius = 3f;

    [Header("Progression")]
    [HideInInspector] public int currentLevel = 1;
    public int maxLevel = 5;

    public void LevelUp()
    {
        if (currentLevel >= maxLevel) return;
        currentLevel++;
        baseDamage = Mathf.RoundToInt(baseDamage * 1.1f);
        cooldown *= 0.95f;
        radius *= 1.1f;
        projectileSpeed *= 1.1f;
    }
}

