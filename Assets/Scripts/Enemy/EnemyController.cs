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
    public GameObject experienceOrbPrefab;  // Prefab de l’orbe d’XP à lâcher à la mort
    public Slider healthBar;                // Slider World-Space pour la barre de vie

    int currentHP;
    Transform player;
    Rigidbody rb;

    void Awake()
    {
        currentHP = maxHP;
        rb = GetComponent<Rigidbody>();

        // Recherche du joueur par tag
        var playerGO = GameObject.FindWithTag("Player");
        if (playerGO == null)
        {
            Debug.LogError("[EnemyController] Aucun GameObject taggé \"Player\" n'a été trouvé dans la scène !", this);
        }
        else
        {
            player = playerGO.transform;
        }

        // Initialise la barre de vie à 100%
        if (healthBar != null)
            healthBar.value = 1f;
    }

    void FixedUpdate()
    {
        if (player == null) 
            return;

        // Se déplace vers le joueur
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

    /// <summary>
    /// Inflige des dégâts à l’ennemi depuis d’autres scripts (projectile, raycast, etc.)
    /// et met à jour la barre de vie.
    /// </summary>
    public void TakeDamage(int amount)
    {
        currentHP = Mathf.Max(currentHP - amount, 0);

        // Mise à jour de la barre de vie
        if (healthBar != null)
            healthBar.value = currentHP / (float)maxHP;

        if (currentHP <= 0)
            Die();
    }

    /// <summary>
    /// Gère la mort : lâche un orbe d’XP, puis détruit l’ennemi.
    /// </summary>
    void Die()
    {
        if (experienceOrbPrefab != null)
            Instantiate(experienceOrbPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}

