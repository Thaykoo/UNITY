using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Références")]
    [Tooltip("Prefab de l'ennemi à instancier")]
    public GameObject enemyPrefab;
    [Tooltip("Cochez ici le Layer de votre sol (Terrain, Plane…)")]
    public LayerMask groundLayer;

    [Header("Réglages Spawn")]
    [Tooltip("Intervalle (en secondes) entre chaque spawn")]
    public float spawnInterval = 3f;
    [Tooltip("Rayon (en unités) autour du spawner pour le spawn aléatoire")]
    public float spawnRadius   = 8f;
    [Tooltip("Hauteur max (en unités) au-dessus du sol pour lancer le Raycast")]
    public float maxRayHeight  = 10f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        // 1) Choix d'un point XZ aléatoire autour du spawner
        Vector2 circle = Random.insideUnitCircle * spawnRadius;
        Vector3 targetXZ = transform.position + new Vector3(circle.x, 0f, circle.y);

        // 2) Raycast vers le bas pour trouver la position exacte du sol
        Vector3 rayStart = targetXZ + Vector3.up * maxRayHeight;
        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, maxRayHeight * 2f, groundLayer))
        {
            // Spawn exactement au contact du sol
            Instantiate(enemyPrefab, hit.point, Quaternion.identity);
        }
        else
        {
            // Si le Raycast échoue, on spawne au niveau Y = 0 (sol par défaut)
            Vector3 fallbackPos = new Vector3(targetXZ.x, 0f, targetXZ.z);
            Instantiate(enemyPrefab, fallbackPos, Quaternion.identity);
        }
    }
}

