using System.Diagnostics;
using UnityEngine;

public class PressureButton : MonoBehaviour
{
    public Transform door;
    public Vector3 openOffset = new Vector3(0, 2, 0);
    public float openSpeed = 2f;

    private Vector3 doorClosedPos;
    private Vector3 doorOpenPos;
    private bool isPressed = false;

    private void Start()
    {
        doorClosedPos = door.position;
        doorOpenPos = door.position + openOffset;
    }

    private void Update()
    {
        if (isPressed)
        {
            door.position = Vector3.Lerp(door.position, doorOpenPos, Time.deltaTime * openSpeed);
        }
        else
        {
            door.position = Vector3.Lerp(door.position, doorClosedPos, Time.deltaTime * openSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Weight"))
        {
            isPressed = true;
            UnityEngine.Debug.Log("Butona basıldı");

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Weight"))
        {
            isPressed = false;
            UnityEngine.Debug.Log("Butona basılamadıdı");
        }
    }

    public bool GetIsPressed() => isPressed;
}
