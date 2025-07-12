using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Références")]
    public GameObject enemyPrefab;

    [Header("Réglages Spawn")]
    public float spawnInterval = 3f;
    public float spawnRadius = 8f;

    float timer;

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
        // Génère une position aléatoire autour du spawner
        Vector2 circle = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = transform.position + new Vector3(circle.x, 0f, circle.y);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
