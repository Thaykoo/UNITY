using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Vitesse & Rotation")]
    [Tooltip("Vitesse de déplacement")]
    public float moveSpeed = 5f;
    [Tooltip("Vitesse de rotation (plus c'est élevé, plus la rotation est rapide)")]
    public float turnSpeed = 10f;

    Rigidbody rb;
    Vector3 inputDir;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        inputDir = new Vector3(h, 0f, v).normalized;
    }

    void FixedUpdate()
    {
        if (inputDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(inputDir, Vector3.up);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, turnSpeed * Time.fixedDeltaTime));
            Vector3 forwardMove = transform.forward * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMove);
        }
    }
}

