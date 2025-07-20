using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Références")]
    public GameObject enemyPrefab;
    public LayerMask groundLayer;

    [Header("Réglages Spawn")]
    public float spawnInterval = 3f;
    public float spawnRadius   = 30f;
    public float maxRayHeight  = 10f;
    [Tooltip("Nombre d'ennemis à spawn à chaque intervalle")]
    public int spawnCount = 5;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            for (int i = 0; i < spawnCount; i++)
                SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Vector2 circle     = Random.insideUnitCircle * spawnRadius;
        Vector3 targetXZ   = transform.position + new Vector3(circle.x, 0f, circle.y);
        Vector3 rayStart   = targetXZ + Vector3.up * maxRayHeight;
        float rayDistance  = maxRayHeight * 2f;

        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, rayDistance, groundLayer))
            Instantiate(enemyPrefab, hit.point, Quaternion.identity);
        else
            Instantiate(enemyPrefab, new Vector3(targetXZ.x, 0f, targetXZ.z), Quaternion.identity);
    }
}

