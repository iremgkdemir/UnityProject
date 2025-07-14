using UnityEngine;

public class TopTrigger : MonoBehaviour
{
    public TopSpawner spawner;
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            spawner.StartSpawning();
            triggered = true;
        }
    }
}
