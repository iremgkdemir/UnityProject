using UnityEngine;

public class KeyController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.hasKey = true;
                Debug.Log("Anahtar alýndý!");
                Destroy(gameObject);
            }
        }
    }
}