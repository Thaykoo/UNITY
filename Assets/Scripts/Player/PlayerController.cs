using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    [Header("Mouvement")]
    public float moveSpeed = 5f;

    Rigidbody rb;
    Vector3 moveInput;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        // Récupère les axes configurés dans Input Manager (Horizontal = A/D ou flèches, Vertical = W/S ou flèches)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveInput = new Vector3(h, 0f, v).normalized;
    }

    void FixedUpdate() {
        // Déplace le rigidbody
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}

