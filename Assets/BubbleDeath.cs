using UnityEngine;

public class BubbleDeath : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private SphereCollider sphereCollider;
    private BubbleMovement movementScript;

    private Rigidbody rb;

    public float popDuration;

    //public CinemachineCamera virtualCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        sphereCollider = GetComponent<SphereCollider>();
        movementScript = GetComponent<BubbleMovement>();

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(Death());
        

        //if (collision.gameObject.CompareTag("TargetTag"))
        //{
        //    gameObject.SetActive(false);
        //}
    }


    private System.Collections.IEnumerator Death()
    {
        Vector3 originalScale = transform.localScale;
        float elapsedTime = 0f;
        Vector3 targetScale = originalScale * 3f; // Target size is 3x original size

        //virtualCamera.enabled = false;

        // Gradually grow the object over time
        while (elapsedTime < popDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / popDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the final scale is exactly the target scale
        transform.localScale = targetScale;

        meshRenderer.enabled = false;
        sphereCollider.enabled = false;
        movementScript.enabled = false;

        rb.isKinematic = true;
        rb.detectCollisions = false;
    }
}
