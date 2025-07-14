using UnityEngine;

public class PressureButtonVisual : MonoBehaviour
{
    public Transform buttonTop;
    public Vector3 pressedOffset = new Vector3(0, -0.1f, 0);

    private Vector3 initialPosition;
    private PressureButton button;

    private void Start()
    {
        button = GetComponent<PressureButton>();
        initialPosition = buttonTop.position; 
    }

    private void Update()
    {
        if (button.GetIsPressed())
        {
            buttonTop.position = Vector3.Lerp(buttonTop.position, initialPosition + pressedOffset, Time.deltaTime * 10);
        }
        else
        {
            buttonTop.position = Vector3.Lerp(buttonTop.position, initialPosition, Time.deltaTime * 10);
        }
    }
}
