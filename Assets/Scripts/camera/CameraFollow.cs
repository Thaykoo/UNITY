using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("La cible à suivre")]
    public Transform target;            // Assigne ton Player ici

    [Header("Offset de position")]
    public Vector3 offset = new Vector3(0f, 3f, -6f);

    [Header("Lissage")]
    [Tooltip("Durée approximative pour atteindre la position cible")]
    public float smoothTime = 0.3f;

    // Variable interne pour SmoothDamp
    Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // La position désirée
        Vector3 desiredPos = target.position + offset;
        // Déplace en douceur de la position actuelle vers la désirée
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPos,
            ref velocity,
            smoothTime
        );

        // (Optionnel) Regarder toujours le joueur
        transform.LookAt(target.position + Vector3.up * 1f);
    }
}

