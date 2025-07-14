using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Vector3 lastCheckpointPosition;

    public bool hasKey = false;
    public int rescuedAnimals = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddRescuedAnimal()
    {
        rescuedAnimals++;
    }
}