using UnityEngine;
using UnityEngine.InputSystem;

public class AnimalInteract : MonoBehaviour
{
    private bool isNearPlayer = false;
    private Transform playerTransform;
    private bool isFollowing = false;

    [Header("Takip Ayarlarý")]
    public float followSpeed = 3f;
    public Vector2 followOffset = new Vector2(0f, 2f); // Oyuncunun biraz üstünde

    void Update()
    {
        if (!isFollowing && isNearPlayer && Keyboard.current.eKey.wasPressedThisFrame)
        {
            isFollowing = true;
            Debug.Log("Ruhani hayvan seni takip etmeye baþladý!");
        }

        if (isFollowing && playerTransform != null)
        {
            Vector2 targetPos = (Vector2)playerTransform.position + followOffset;
            transform.position = Vector2.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = true;
            playerTransform = other.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = false;
        }
    }
}
