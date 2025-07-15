using UnityEngine;

public class PressureButtonVisual : MonoBehaviour
{
    public Transform buttonTop;
    public Vector3 pressedOffset = new Vector3(0, -0.1f, 0);

    private Vector3 initialLocalPos;
    private PressureButton button;

    private void Start()
    {
        button = GetComponent<PressureButton>();
        initialLocalPos = buttonTop.localPosition;
    }

    private void Update()
    {
        Vector3 targetOffset = button.GetIsPressed()
            ? initialLocalPos + pressedOffset
            : initialLocalPos;

        buttonTop.localPosition = Vector3.Lerp(buttonTop.localPosition, targetOffset, Time.deltaTime * 10);
    }
}
