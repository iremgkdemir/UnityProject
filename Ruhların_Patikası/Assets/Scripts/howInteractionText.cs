using UnityEngine;

public class ShowInteractionText2D : MonoBehaviour
{
    public GameObject interactionText;

    private bool playerInRange = false;
    private bool hasPressedE = false;

    private void Start()
    {
        if (interactionText != null)
            interactionText.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !hasPressedE && Input.GetKeyDown(KeyCode.E))
        {
            interactionText.SetActive(false);
            hasPressedE = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPressedE)
        {
            playerInRange = true;
            interactionText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (!hasPressedE && interactionText != null)
                interactionText.SetActive(false);
        }
    }
}
