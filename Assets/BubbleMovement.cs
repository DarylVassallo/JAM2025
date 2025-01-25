using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    public float jumpForce = 0.000000000000000000000000000000001f;
    public float fallSpeed = 1f; // The downward speed, lower values = slower fall
    public float drag = 10f; // Adjust drag for realistic air resistance
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the Rigidbody component is assigned or found
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();

            rb.useGravity = false; // Disable Unity's default gravity
            rb.linearDamping = drag; // Set drag for air resistance
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector3.down * fallSpeed, ForceMode.Acceleration);

        // Apply a constant force while the spacebar is held down
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("JUMP: " + Vector3.up * jumpForce * Time.deltaTime);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Force);
        }
    }
}
