using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    public float breakDelay = 0.5f; 

    private bool isBreaking = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isBreaking && collision.collider.CompareTag("Player"))
        {
            isBreaking = true;
            Invoke("BreakPlatform", breakDelay); 
        }
    }

    void BreakPlatform()
    {
        gameObject.SetActive(false); 
    }
}
