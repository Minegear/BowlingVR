using UnityEngine;

public class TestDeplacement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;
    public Transform cameraTransform;

    float rotationX = 0f;
    float rotationY = 0f;

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // --- Déplacement horizontal ---
        float moveX = Input.GetAxis("Horizontal");  // A/D ou Q/D
        float moveZ = Input.GetAxis("Vertical");    // W/S ou Z/S

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.position += move * moveSpeed * Time.deltaTime;

        // --- Rotation avec la souris ---
        rotationX += Input.GetAxis("Mouse X") * lookSpeed;
        rotationY -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationY = Mathf.Clamp(rotationY, -80f, 80f); // limite regard vertical

        transform.rotation = Quaternion.Euler(0f, rotationX, 0f);
        cameraTransform.localRotation = Quaternion.Euler(rotationY, 0f, 0f);
    }
}
