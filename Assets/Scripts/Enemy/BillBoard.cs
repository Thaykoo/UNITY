using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera cam;
    void Start() => cam = Camera.main;
    void LateUpdate()
    {
        if (cam != null)
            transform.forward = cam.transform.forward;
    }
}

