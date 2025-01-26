using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    public float jumpForce = 10f;
    public float fallSpeed = 1f;
    public float constantForwardForce = 1f;
    public float mouseClickForwardForce = 20f;
    public float drag = 10f;

    private Rigidbody rb;
    private Camera mainCamera;

    public float clickCooldown = 0.5f;
    private float timeFromLastClick = 0f;
    private Vector3 pushVelocity = Vector3.zero;

    public float jumpCooldown = 0.5f;
    private float timeFromLastJump = 0f;
    private Vector3 jumpVelocity = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        mainCamera = Camera.main; // Reference to the main camera
    }

    void Update()
    {
        // Update time trackers
        timeFromLastClick += Time.deltaTime;
        timeFromLastJump += Time.deltaTime;

        // Use the camera's forward direction for constant movement
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0; // Restrict movement to a plane
        cameraForward.Normalize();

        Vector3 baseVelocity = cameraForward * constantForwardForce;

        // Apply downward force for falling
        rb.AddForce(Vector3.down * fallSpeed, ForceMode.Acceleration);

        // Left mouse button for forward burst
        if (Input.GetMouseButtonDown(0) && timeFromLastClick >= clickCooldown)
        {
            pushVelocity = cameraForward * mouseClickForwardForce;
            timeFromLastClick = 0f;
        }

        // Right mouse button for upward burst
        if (Input.GetMouseButtonDown(1) && timeFromLastJump >= jumpCooldown)
        {
            jumpVelocity = Vector3.up * jumpForce;
            timeFromLastJump = 0f;
        }

        // Smoothly decay user input forces
        pushVelocity = Vector3.Lerp(pushVelocity, Vector3.zero, Time.deltaTime * drag);
        jumpVelocity = Vector3.Lerp(jumpVelocity, Vector3.zero, Time.deltaTime * drag);

        // Apply the resulting velocity (wind effect will be added externally)
        rb.linearVelocity = baseVelocity + pushVelocity + jumpVelocity;
    }
}











/*using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    public float jumpForce = 10f;
    public float fallSpeed = 1f;
    public float constantForwardForce = 1f;
    public float mouseClickForwardForce = 20f;
    public float drag = 10f;

    private Rigidbody rb;
    private Camera mainCamera;

    public float clickCooldown = 0.5f;
    private float timeFromLastClick = 0f;
    private float pushVelocity = 1;

    public float jumpCooldown = 0.5f;
    private float timeFromLastJump = 0f;
    private float jumpVelocity = 1;


    public float maxVelocity;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        mainCamera = Camera.main; // Reference to the main camera
    }

    void Update()
    {
        // Update time trackers
        timeFromLastClick += Time.deltaTime;
        timeFromLastJump += Time.deltaTime;

        // Use the camera's forward direction for constant movement
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0; // Restrict movement to a plane
        cameraForward.Normalize();

        Vector3 baseVelocity = cameraForward * constantForwardForce;

        // Left mouse button for forward burst
        if (Input.GetMouseButton(0))
        {   
            pushVelocity = mouseClickForwardForce;
        }
        else
        {
            pushVelocity = 0;
        }

        // Right mouse button for upward burst
        if (Input.GetMouseButton(1))
        {
            jumpVelocity = jumpForce;
        }
        else
        {
            jumpVelocity = 0;
        }

        rb.AddForce(cameraForward * pushVelocity, ForceMode.Force);
        rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Force);

        if (rb.linearVelocity.magnitude > maxVelocity)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxVelocity;
        }

        rb.linearVelocity = new Vector3(rb.linearVelocity.x * drag, rb.linearVelocity.y * drag, rb.linearVelocity.z * drag);

        // Apply downward force for falling
        rb.AddForce(Vector3.down * fallSpeed, ForceMode.Acceleration);
    }
}
*/
