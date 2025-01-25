using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    public float jumpForce = 1f;
    public float fallSpeed = 1f;
    public float constantForwardForce = 1f; // Renamed to clarify its purpose
    public float mouseClickForwardForce = 20f; // Separate variable for mouse click forward force
    public float drag = 10f;
    private Rigidbody rb;

    public float sensitivity = 0.1f;
    private Vector3 lastMousePosition;

    private Camera mainCamera; // Reference to the camera

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearDamping = drag;

        lastMousePosition = Input.mousePosition;

        mainCamera = Camera.main; // Get the main camera
    }

    public float clickCooldown = 0.5f; // Cooldown time between clicks
    private float timeFromLastClick = 0f; // Tracks time since the last click
    private Vector3 pushVelocity = Vector3.zero; // Tracks the additional velocity from the push

    void Update()
    {
        // Update timeFromLastClick
        timeFromLastClick += Time.deltaTime;

        // Use the camera's forward direction for movement
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0; // Ignore the vertical component to keep movement on a plane
        cameraForward.Normalize();

        // Constant forward velocity based on the camera's forward direction
        Vector3 baseVelocity = cameraForward * constantForwardForce;

        // Apply downward force for falling
        rb.AddForce(Vector3.down * fallSpeed, ForceMode.Acceleration);

        // Handle rotation based on mouse movement
        Vector3 currentMousePosition = Input.mousePosition;
        float mouseDeltaX = currentMousePosition.x - lastMousePosition.x;
        transform.Rotate(Vector3.up * mouseDeltaX * sensitivity);

        // Handle jump when the space key is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Force);
        }

        // Apply additional velocity when the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            if (timeFromLastClick >= clickCooldown) // Ensure cooldown has passed
            {
                pushVelocity = cameraForward * mouseClickForwardForce; // Add a velocity push
                timeFromLastClick = 0f; // Reset time since last click
            }
        }

        // Smoothly decay the push velocity over time for an organic float effect
        pushVelocity = Vector3.Lerp(pushVelocity, Vector3.zero, Time.deltaTime * drag);

        // Combine base velocity with the push velocity
        rb.linearVelocity = baseVelocity + pushVelocity;

        lastMousePosition = currentMousePosition;
    }

}
