using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
    public static PlayerAbilityManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    [Header("Prefabs")]
    public GameObject fireballPrefab;
    public GameObject shotSpherePrefab;
    public GameObject lifeDrainOrbPrefab;

    [Header("Enemy Layer")]
    public LayerMask enemyLayer;

    Dictionary<UpgradeType, UpgradeSO> activeUpgrades = new();

    public void ApplyUpgrade(UpgradeSO so)
    {
        if (!activeUpgrades.ContainsKey(so.type))
        {
            var clone = Instantiate(so);
            activeUpgrades[so.type] = clone;
            InitializeAbility(clone);
        }
        else
        {
            var existing = activeUpgrades[so.type];
            existing.LevelUp();
            UpdateAbility(existing);
        }
    }

    void InitializeAbility(UpgradeSO so)
    {
        switch (so.type)
        {
            case UpgradeType.Fireball:
                StartCoroutine(FireballRoutine(so));
                break;

            case UpgradeType.Shot:
                StartCoroutine(ShotRoutine(so));
                break;

            case UpgradeType.LifeDrain:
                SpawnLifeDrainOrbs(so);
                break;
        }
    }

    void UpdateAbility(UpgradeSO so)
    {
        if (so.type == UpgradeType.Shot)
        {
            var atk = GetComponent<PlayerAttack>();
            atk.attackDamage = so.baseDamage;
            atk.attackRange = so.radius;
            atk.attackCooldown = so.cooldown;
        }
    }

    IEnumerator FireballRoutine(UpgradeSO so)
    {
        while (true)
        {
            var enemies = FindObjectsOfType<EnemyController>()
                          .Select(e => e.transform)
                          .ToArray();

            if (enemies.Length > 0)
            {
                var closest = enemies
                              .OrderBy(t => Vector3.Distance(transform.position, t.position))
                              .First();

                var go = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
                go.GetComponent<FireballProjectile>()
                  .Initialize(closest, so.projectileSpeed, so.baseDamage);
            }

            yield return new WaitForSeconds(so.cooldown);
        }
    }

    IEnumerator ShotRoutine(UpgradeSO so)
    {
        while (true)
        {
            var go = Instantiate(shotSpherePrefab, transform.position + transform.forward, Quaternion.identity);
            go.GetComponent<ShotProjectile>().Initialize(transform.forward, so.projectileSpeed, so.baseDamage);
            yield return new WaitForSeconds(so.cooldown);
        }
    }

    void SpawnLifeDrainOrbs(UpgradeSO so)
    {
        int orbCount = 4;
        float angleStep = 2 * Mathf.PI / orbCount;

        for (int i = 0; i < orbCount; i++)
        {
            float angle = i * angleStep;

            var orb = Instantiate(lifeDrainOrbPrefab, transform.position, Quaternion.identity);
            orb.GetComponent<LifeDrainOrb>()
               .Initialize(transform, angle, so.radius, 2f, so.baseDamage, GetComponent<Health>());
        }
    }
}

