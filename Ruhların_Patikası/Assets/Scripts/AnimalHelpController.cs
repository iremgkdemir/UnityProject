using UnityEngine;
using UnityEngine.InputSystem;

public class AnimalHelpController : MonoBehaviour
{
    private bool isNearPlayer = false;
    private PlayerController player;

    [Header("Yardým Türü")]
    public string requiredItemTag = "Key"; // "Key" = kafes, "Food" = besleme

    [Header("Görseller")]
    public GameObject cageVisual; // varsa kafes
    public GameObject animalObject; // hayvan sprite'ý

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
                Debug.Log($"Yardým için gerekli item yok: {requiredItemTag}");
            }
        }
    }

    void HelpAnimal()
    {
        Debug.Log("Hayvana yardým edildi!");

        if (cageVisual != null)
            cageVisual.SetActive(false); // Kafes varsa kaldýr

        if (animalObject != null)
            StartCoroutine(HideAnimalDelayed());

        GameManager.Instance.rescuedAnimals++;
        isHelped = true;
    }

    System.Collections.IEnumerator HideAnimalDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        animalObject.SetActive(false);
        Debug.Log("Hayvan artýk güvende!");
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
