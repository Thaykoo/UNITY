using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FireballProjectile : MonoBehaviour
{
    Transform target;
    float speed;
    int damage;

    /// <summary>
    /// À appeler immédiatement après Instantiate()
    /// </summary>
    public void Initialize(Transform target, float speed, int damage)
    {
        this.target = target;
        this.speed  = speed;
        this.damage = damage;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Bouge vers la cible
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        // Si on atteind ou dépasse la cible
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            var ec = target.GetComponent<EnemyController>();
            if (ec != null) ec.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}

