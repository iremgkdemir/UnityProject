using UnityEngine;
using UnityEngine.InputSystem;

public class AnimalInteract : MonoBehaviour
{
    private bool isNearPlayer = false;
    private Transform playerTransform;
    private SmartFollower follower;

    void Start()
    {
        follower = GetComponent<SmartFollower>();
    }

    void Update()
    {
        if (isNearPlayer && Keyboard.current.eKey.wasPressedThisFrame)
        {
            follower.StartFollowing(playerTransform); 
            Debug.Log("Hayvan havalandý ve takip etmeye baþladý!");
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
