using UnityEngine;

[RequireComponent(typeof(Transform))]
public class PlayerAttack : MonoBehaviour
{
    [Header("Paramètres d'attaque")]
    public KeyCode attackKey = KeyCode.Space;
    public float attackRange = 2f;
    public int attackDamage = 10;
    public float attackCooldown = 0.5f;

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
        RaycastHit hit;
        // Lance un rayon depuis la position du joueur vers l’avant
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
        {
            // Cherche un script EnemyController sur l’objet touché
            var enemy = hit.collider.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
                Debug.Log($"Tu as infligé {attackDamage} dégâts à {enemy.name}");
            }
        }
    }

    // Pour visualiser la portée dans la Scene View
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * attackRange);
    }
}

