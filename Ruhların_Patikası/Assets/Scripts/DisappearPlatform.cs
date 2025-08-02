using UnityEngine;

public class DisappearPlatform : MonoBehaviour
{
    private Animator animator;
    public float destroyDelay = 1f;
    private bool triggered = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered && collision.gameObject.CompareTag("Player"))
        {
            triggered = true;
            animator.SetTrigger("Disappear");
            Invoke(nameof(DestroyPlatform), destroyDelay); // animasyon bitince yok olsun
        }
    }

    void DestroyPlatform()
    {
        Destroy(gameObject); // veya gameObject.SetActive(false);
    }
}
