[System.Serializable]
public class PlayerUpgrade
{
    public UpgradeType type;
    public float baseDamage;
    public float cooldown;
    public float radius;
    public float projectileSpeed;
    public int level = 1;

    public void LevelUp()
    {
        level++;
        baseDamage *= 1.2f;
        cooldown *= 0.9f;
        radius += 0.2f;
        projectileSpeed *= 1.1f;
    }
}

