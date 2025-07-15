using UnityEngine;

public class ItemController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            switch (gameObject.tag)
            {
                case "Key":
                    player.hasKey = true;
                    Debug.Log("Anahtar alýndý!");
                    break;

                case "Food":
                    player.hasFood = true;
                    Debug.Log("Yiyecek alýndý!");
                    break;
            }

            Destroy(gameObject);
        }
    }

}
