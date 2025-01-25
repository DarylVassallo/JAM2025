using UnityEngine;

public class BubbleSizer : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private SphereCollider sphereCollider;
    private BubbleMovement movementScript;

    private Rigidbody rb;

    public float popDuration;
    public float shrink;

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

    private void OnCollisionEnter(Collision other)
    {
        float otherVolume = other.gameObject.transform.localScale.x * other.gameObject.transform.localScale.y * other.gameObject.transform.localScale.z;
        float volume = this.transform.localScale.x * this.transform.localScale.y * this.transform.localScale.z;

        Debug.Log("OTHER VOLUME: " + otherVolume);
        Debug.Log("VOLUME: " + volume);
        if (otherVolume > volume)
        {
            Debug.Log("DEATH!!!!!!!!!!!!!!!!!!");
            StartCoroutine(Death());
        }
        else
        {
            Debug.Log("CONSUME");
            float finalVolume = volume + otherVolume;
            this.transform.localScale = new Vector3(finalVolume * transform.localScale.x / volume,
                                                finalVolume * transform.localScale.y / volume,
                                                finalVolume * transform.localScale.z / volume);
            Destroy(other.gameObject);
            volume = this.transform.localScale.x * this.transform.localScale.y * this.transform.localScale.z;
            Debug.Log("NEWVOLUME: " + volume);
        }


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
