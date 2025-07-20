using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
    public static PlayerAbilityManager instance;

    public GameObject fireballPrefab;
    public GameObject shotSpherePrefab;
    public GameObject lifeDrainOrbPrefab;

    public LayerMask enemyLayer;

    private Dictionary<UpgradeType, PlayerUpgrade> activeUpgrades = new();

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    public void ApplyUpgrade(UpgradeType type)
    {
        if (activeUpgrades.ContainsKey(type))
        {
            activeUpgrades[type].LevelUp();
        }
        else
        {
            var upgrade = new PlayerUpgrade
            {
                type = type,
                baseDamage = 20f,
                cooldown = 1f,
                radius = 3f,
                projectileSpeed = 15f
            };
            activeUpgrades[type] = upgrade;
            LaunchAttack(type);
        }
    }

    public int GetUpgradeLevel(UpgradeType type)
    {
        if (activeUpgrades.TryGetValue(type, out var upgrade))
            return upgrade.level;
        return 0;
    }

    void LaunchAttack(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.Fireball:
                StartCoroutine(FireballRoutine());
                break;
            case UpgradeType.Shot:
                StartCoroutine(ShotRoutine());
                break;
            case UpgradeType.LifeDrain:
                SpawnLifeDrainOrbs();
                break;
        }
    }

    IEnumerator FireballRoutine()
    {
        var up = activeUpgrades[UpgradeType.Fireball];

        while (true)
        {
            var enemies = FindObjectsOfType<EnemyController>()
                          .Select(e => e.transform)
                          .ToArray();
            if (enemies.Length > 0)
            {
                var closest = enemies.OrderBy(t => Vector3.Distance(transform.position, t.position)).First();
                var go = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
                go.GetComponent<FireballProjectile>()
                  .Initialize(closest, up.projectileSpeed, (int)up.baseDamage);
            }
            yield return new WaitForSeconds(up.cooldown);
        }
    }

    IEnumerator ShotRoutine()
    {
        var up = activeUpgrades[UpgradeType.Shot];

        while (true)
        {
            var go = Instantiate(shotSpherePrefab, transform.position + transform.forward, Quaternion.identity);
            go.GetComponent<ShotProjectile>()
              .Initialize(transform.forward, up.projectileSpeed, (int)up.baseDamage);
            yield return new WaitForSeconds(up.cooldown);
        }
    }

    void SpawnLifeDrainOrbs()
    {
        var up = activeUpgrades[UpgradeType.LifeDrain];
        int orbCount = 4;
        float angleStep = 2 * Mathf.PI / orbCount;

        for (int i = 0; i < orbCount; i++)
        {
            float angle = i * angleStep;
            var orb = Instantiate(lifeDrainOrbPrefab, transform.position, Quaternion.identity);
            orb.GetComponent<LifeDrainOrb>()
               .Initialize(transform, angle, up.radius, 2f, (int)up.baseDamage, GetComponent<Health>());
        }
    }
}

