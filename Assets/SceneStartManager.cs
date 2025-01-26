using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [Header("References")]
    public GameObject player;               // Assign the player GameObject in the Inspector
    public CanvasGroup fadeCanvasGroup;     // Assign a CanvasGroup for fade effects

    [Header("Force Settings")]
    public float upwardForce = 500f;        // Upward force to apply to the player

    [Header("Fade Settings")]
    public float fadeDuration = 1f;         // Duration of the fade-out (in seconds)

    private Rigidbody playerRigidbody;      // Rigidbody of the player

    void Start()
    {
        // Ensure the player GameObject and Rigidbody are assigned
        if (player == null)
        {
            Debug.LogError("Player GameObject is not assigned in the Inspector!");
            return;
        }

        playerRigidbody = player.GetComponent<Rigidbody>();
        if (playerRigidbody == null)
        {
            Debug.LogError("The assigned Player GameObject does not have a Rigidbody!");
            return;
        }

        //player.GetComponent<BubbleMovement>().enabled = false;

        // Start the scene fade-out and force application
        StartCoroutine(SceneStartSequence());
    }

    private IEnumerator SceneStartSequence()
    {
        // Start with the screen fully black
        fadeCanvasGroup.alpha = 1f;

        StartCoroutine(EnableBubbleMovementAfterDelay());

        // Fade out to reveal the scene
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = 1f - (elapsedTime / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = 0f; // Ensure the screen is fully visible at the end

        // Apply an upward force to the player
        playerRigidbody.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);

    }

    private IEnumerator EnableBubbleMovementAfterDelay()
    {
        yield return new WaitForSeconds(fadeDuration); // Wait for the fade-in duration
        player.GetComponent<BubbleMovement>().enabled = true;
    }

}
