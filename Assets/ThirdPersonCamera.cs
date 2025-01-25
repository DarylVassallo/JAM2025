using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Sensitivity for mouse movement
    public Transform playerBody;         // Reference to the player's body for horizontal rotation
    public bool lockCursor = true;       // Whether to lock the cursor in the center of the screen

    private float xRotation = 0f;        // Tracks vertical rotation to clamp it

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center
            Cursor.visible = false;                  // Hide the cursor
        }
    }

    void Update()
    {
        // Get mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Adjust vertical rotation (clamp to avoid flipping)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply rotations
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Vertical rotation
        playerBody.Rotate(Vector3.up * mouseX);                        // Horizontal rotation
    }
}
