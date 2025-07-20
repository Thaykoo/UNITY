using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("La cible à suivre")]
    public Transform target;

    [Header("Offset de position")]
    public Vector3 offset = new Vector3(0f, 3f, -6f);

    [Header("Lissage")]
    [Tooltip("Durée approximative pour atteindre la position cible")]
    public float smoothTime = 0.3f;

    Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPos,
            ref velocity,
            smoothTime
        );

        transform.LookAt(target.position + Vector3.up * 1f);
    }
}

