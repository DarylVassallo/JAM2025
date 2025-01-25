using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    public float jumpForce = 1f;
    public float fallSpeed = 1f;
    public float forwardForce = 1f;
    public float drag = 10f;
    private Rigidbody rb;


    public float sensitivity = 0.1f;
    private Vector3 lastMousePosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.linearDamping = drag;

        lastMousePosition = Input.mousePosition;

        //rb.AddForce(transform.forward * forwardForce, ForceMode.Force);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentVelocity = rb.linearVelocity;
        rb.linearVelocity = new Vector3(currentVelocity.x, currentVelocity.y, forwardForce);

        rb.AddForce(Vector3.down * fallSpeed, ForceMode.Acceleration);
        

        Vector3 currentMousePosition = Input.mousePosition;
        float mouseDeltaX = currentMousePosition.x - lastMousePosition.x;
        rb.AddForce(Vector3.right * mouseDeltaX * sensitivity, ForceMode.Force);

        if (Input.GetKey(KeyCode.Space))
        {
            //Debug.Log("JUMP: " + Vector3.up * jumpForce * Time.deltaTime);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Force);
        }

        lastMousePosition = currentMousePosition;

        //Debug.Log("Current Velocity: " + rb.linearVelocity);
    }
}
