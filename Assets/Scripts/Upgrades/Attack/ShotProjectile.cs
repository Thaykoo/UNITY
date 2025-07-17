using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ShotProjectile : MonoBehaviour
{
    Vector3 direction;
    float speed;
    int damage;

    /// <summary>
    /// À appeler juste après Instantiate()
    /// </summary>
    public void Initialize(Vector3 dir, float speed, int damage)
    {
        this.direction = dir.normalized;
        this.speed     = speed;
        this.damage    = damage;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        var ec = other.GetComponent<EnemyController>();
        if (ec != null)
        {
            ec.TakeDamage(damage);
            Destroy(gameObject);
        }
        // Sinon, détruire la balle si elle touche un obstacle (optionnel)
        else if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

