using UnityEngine;

public class WindEffect : MonoBehaviour
{
    public float windForce = 2f; // Strength of the wind gusts
    public float minWindInterval = 2f; // Minimum time between gusts
    public float maxWindInterval = 5f; // Maximum time between gusts
    public float drag = 10f; // Smooth decay for wind effects

    private Rigidbody rb;
    private Vector3 windVelocity = Vector3.zero;
    private float timeUntilNextWind = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Initialize the first wind gust interval
        SetNextWindInterval();
    }

    void Update()
    {
        // Count down to the next wind gust
        timeUntilNextWind -= Time.deltaTime;

        if (timeUntilNextWind <= 0f)
        {
            // Generate a random direction for the wind gust
            Vector3 randomDirection = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-0.5f, 0.5f), // Slight vertical influence
                Random.Range(-1f, 1f)
            ).normalized;

            windVelocity = randomDirection * windForce;

            // Set the next wind interval
            SetNextWindInterval();
        }

        // Smoothly decay the wind velocity over time
        windVelocity = Vector3.Lerp(windVelocity, Vector3.zero, Time.deltaTime * drag);

        // Apply the wind force to the Rigidbody
        rb.AddForce(windVelocity, ForceMode.Force);
    }

    // Randomly determine the interval for the next wind gust
    private void SetNextWindInterval()
    {
        timeUntilNextWind = Random.Range(minWindInterval, maxWindInterval);
    }
}
