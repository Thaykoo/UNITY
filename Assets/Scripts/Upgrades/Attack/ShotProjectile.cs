using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ShotProjectile : MonoBehaviour
{
    Vector3 direction;
    float speed;
    int damage;

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
        else if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

