using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    public int xpValue = 1;
    public float floatSpeed = 1f;  // optionnel : effet de flottement

    void Update()
    {
        // Léger mouvement de bascule
        transform.position += Vector3.up * Mathf.Sin(Time.time * floatSpeed) * 0.001f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var stats = other.GetComponent<PlayerStats>();
            if (stats != null)
                stats.GainXP(xpValue);
            Destroy(gameObject);
        }
    }
}

