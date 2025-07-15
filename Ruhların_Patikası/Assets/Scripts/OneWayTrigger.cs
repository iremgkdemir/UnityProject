using UnityEngine;

[RequireComponent(typeof(PressureButton))]
public class OneWayTrigger : MonoBehaviour
{
    private PressureButton button;
    private bool locked = false;

    private void Awake()
    {
        button = GetComponent<PressureButton>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") || collision.CompareTag("Weight")) && !locked)
        {
            button.SendMessage("SetPressed", true);
            locked = true;
        }
    }
}
