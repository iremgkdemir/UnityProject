using UnityEngine;
using UnityEngine.InputSystem;

public class AnimalHelpController : MonoBehaviour
{
    private bool isNearPlayer = false;
    private PlayerController player;

    [Header("Yard�m T�r�")]
    public string requiredItemTag = "Key"; // "Key" = kafes, "Food" = besleme

    [Header("G�rseller")]
    public GameObject cageVisual; // varsa kafes
    public GameObject animalObject; // hayvan sprite'�

    private bool isHelped = false;

    void Update()
    {
        if (isNearPlayer && Keyboard.current.eKey.wasPressedThisFrame && !isHelped)
        {
            if (player == null) return;

            if (requiredItemTag == "Key" && player.hasKey)
            {
                player.hasKey = false;
                HelpAnimal();
            }
            else if (requiredItemTag == "Food" && player.hasFood)
            {
                player.hasFood = false;
                HelpAnimal();
            }
            else
            {
                Debug.Log($"Yard�m i�in gerekli item yok: {requiredItemTag}");
            }
        }
    }

    void HelpAnimal()
    {
        Debug.Log("Hayvana yard�m edildi!");

        if (cageVisual != null)
            cageVisual.SetActive(false); // Kafes varsa kald�r

        if (animalObject != null)
            StartCoroutine(HideAnimalDelayed());

        GameManager.Instance.rescuedAnimals++;
        isHelped = true;
    }

    System.Collections.IEnumerator HideAnimalDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        animalObject.SetActive(false);
        Debug.Log("Hayvan art�k g�vende!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = true;
            player = other.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = false;
            player = null;
        }
    }
}
