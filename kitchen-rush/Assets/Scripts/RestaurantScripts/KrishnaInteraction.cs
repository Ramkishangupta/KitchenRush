using UnityEngine;

public class KrishnaInteraction : MonoBehaviour
{
    public GameObject thoughtBubble; // Assign thought1 in Inspector

    private void Start()
    {
        if (thoughtBubble != null)
        {
            thoughtBubble.SetActive(false); // Hide initially
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Glass")) // Ensure Glass has the correct tag
        {
            if (thoughtBubble != null)
            {
                thoughtBubble.SetActive(true); // Show thought bubble
            }

            Destroy(other.gameObject); // Remove Glass from scene
        }
    }
}
