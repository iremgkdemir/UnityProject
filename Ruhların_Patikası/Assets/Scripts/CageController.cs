using UnityEngine;
using UnityEngine.InputSystem;

public class CageController : MonoBehaviour
{
    private bool isNearPlayer = false;
    private PlayerController player;

    [Header("Referanslar")]
    public GameObject cageVisual;     
    public GameObject animalObject;    

    private bool isOpened = false;

    void Update()
    {
        if (isNearPlayer && Keyboard.current.eKey.wasPressedThisFrame && !isOpened)
        {
            if (player != null && player.hasKey)
            {
                OpenCage();
            }
            else
            {
                Debug.Log("Anahtarýn yok, kafes açýlmaz.");
            }
        }
    }

    void OpenCage()
    {
        Debug.Log("Kafes açýldý!");

        if (cageVisual != null)
            cageVisual.SetActive(false); 

        if (animalObject != null)
            StartCoroutine(HideAnimalDelayed()); 

        isOpened = true;
        player.hasKey = false;
        GameManager.Instance.rescuedAnimals++;
    }

    System.Collections.IEnumerator HideAnimalDelayed()
    {
        yield return new WaitForSeconds(0.5f); 
        animalObject.SetActive(false); 
        Debug.Log("Hayvan çantaya girdi!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Kafese yaklaþýldý!");
            isNearPlayer = true;
            player = other.GetComponent<PlayerController>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = false;
            player = null;
        }
    }
}
