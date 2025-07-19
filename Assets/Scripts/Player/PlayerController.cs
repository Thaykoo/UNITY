using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Vitesse & Rotation")]
    [Tooltip("Vitesse de déplacement")]
    public float moveSpeed = 5f;
    [Tooltip("Vitesse de rotation (plus c'est élevé, plus la rotation est rapide)")]
    public float turnSpeed = 10f;

    Rigidbody   rb;
    Vector3     inputDir;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Récupère l'input raw (pas de smoothing)
        float h = Input.GetAxisRaw("Horizontal"); // A/D ou flèches
        float v = Input.GetAxisRaw("Vertical");   // W/S ou flèches

        // Crée un vecteur de direction horizontal
        inputDir = new Vector3(h, 0f, v).normalized;
    }

    void FixedUpdate()
    {
        // Si on a du mouvement
        if (inputDir.sqrMagnitude > 0.001f)
        {
            // 1) Rotation : on regarde vers inputDir
            Quaternion targetRot = Quaternion.LookRotation(inputDir, Vector3.up);
            rb.MoveRotation(
                Quaternion.Slerp(rb.rotation, targetRot, turnSpeed * Time.fixedDeltaTime)
            );

            // 2) Déplacement : avance selon forward (déjà tourné)
            Vector3 forwardMove = transform.forward * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMove);
        }
        // sinon on ne bouge ni ne tourne
    }
}

