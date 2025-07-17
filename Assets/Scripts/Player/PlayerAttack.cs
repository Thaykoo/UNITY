// PlayerAttack.cs
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class PlayerAttack : MonoBehaviour
{
    [Header("Tir manuel")]
    public KeyCode attackKey      = KeyCode.Space;
    public float   attackRange    = 2f;
    public int     attackDamage   = 10;
    public float   attackCooldown = 0.5f;

    [Header("Shot Ability (Sphère orange)")]
    [HideInInspector] public GameObject shotPrefab;
    [HideInInspector] public float       shotSpeed;
    public float       shotLifetime   = 5f;

    float lastAttackTime;

    void Update()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;
        if (Input.GetKeyDown(attackKey))
        {
            lastAttackTime = Time.time;
            DoAttack();
        }
    }

    void DoAttack()
    {
        if (shotPrefab != null)
        {
            Vector3 spawnPos = transform.position + transform.forward * 1f;
            var projGO = Instantiate(shotPrefab, spawnPos, transform.rotation);
            var proj = projGO.GetComponent<ShotProjectile>();
            if (proj != null) proj.Initialize(transform.forward, shotSpeed, attackDamage);
            Destroy(projGO, shotLifetime);
        }
        else
        {
            // Fallback : raycast classique
            if (Physics.Raycast(transform.position, transform.forward, out var hit, attackRange))
            {
                var enemy = hit.collider.GetComponent<EnemyController>();
                if (enemy != null)
                    enemy.TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * attackRange);
    }
}

