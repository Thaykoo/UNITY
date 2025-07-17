using UnityEngine;

public class LifeDrainOrb : MonoBehaviour
{
    Transform center;
    float angle;
    float radius;
    float speed;
    int damage;
    Health playerHealth;

    void Update()
    {
        if (center == null) return;

        // Tourne autour du centre
        angle += speed * Time.deltaTime;
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        transform.position = center.position + new Vector3(x, 0, z);
    }

    public void Initialize(Transform center, float initialAngle, float radius, float speed, int damage, Health playerHealth)
    {
        this.center = center;
        this.angle = initialAngle;
        this.radius = radius;
        this.speed = speed;
        this.damage = damage;
        this.playerHealth = playerHealth;
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyController ec = other.GetComponent<EnemyController>();
        if (ec != null)
        {
            ec.TakeDamage(damage);
            playerHealth.Heal(damage);
        }
    }
}

