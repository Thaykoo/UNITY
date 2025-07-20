using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    [Header("Stats Ennemi")]
    public float speed = 3f;
    public int damage = 10;
    public int maxHP = 20;

    [Header("Références")]
    public GameObject experienceOrbPrefab;
    public Slider healthBar;

    int currentHP;
    Transform player;
    Rigidbody rb;

    void Awake()
    {
        currentHP = maxHP;
        rb = GetComponent<Rigidbody>();
        var playerGO = GameObject.FindWithTag("Player");
        if (playerGO == null)
            Debug.LogError("[EnemyController] Aucun GameObject taggé \"Player\" n'a été trouvé dans la scène !", this);
        else
            player = playerGO.transform;
        if (healthBar != null)
            healthBar.value = 1f;
    }

    void FixedUpdate()
    {
        if (player == null) return;
        Vector3 dir = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var health = other.gameObject.GetComponent<Health>();
            if (health != null)
                health.TakeDamage(damage);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHP = Mathf.Max(currentHP - amount, 0);
        if (healthBar != null)
            healthBar.value = currentHP / (float)maxHP;
        if (currentHP <= 0)
            Die();
    }

    void Die()
    {
        if (experienceOrbPrefab != null)
            Instantiate(experienceOrbPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

