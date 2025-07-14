using UnityEngine;

public class TopSpawner : MonoBehaviour
{
    public GameObject topPrefab;
    public float spawnInterval = 1.2f;
    public int totalToSpawn = 10;
    public Transform spawnPoint;

    private int spawnedCount = 0;
    private float timer = 0f;
    private bool isSpawning = false;

    public void StartSpawning()
    {
        isSpawning = true;
        spawnedCount = 0;

        // Ýlk topu hemen üret
        SpawnBall();
        timer = 0f;
    }

    private void Update()
    {
        if (!isSpawning) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval && spawnedCount < totalToSpawn)
        {
            SpawnBall();
            timer = 0f;
        }

        if (spawnedCount >= totalToSpawn)
        {
            isSpawning = false;
        }
    }

    private void SpawnBall()
    {
        Instantiate(topPrefab, spawnPoint.position, Quaternion.identity);
        spawnedCount++;
    }
}
