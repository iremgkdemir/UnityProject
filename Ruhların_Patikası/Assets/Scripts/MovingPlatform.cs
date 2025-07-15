using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum MovementAxis { Horizontal, Vertical }
    public enum MoveDirection { Positive, Negative }

    [Header("Movement Settings")]
    public MovementAxis axis = MovementAxis.Horizontal;
    public MoveDirection direction = MoveDirection.Positive;
    public float moveDistance = 3f;
    public float speed = 2f;

    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 target;

    void Start()
    {
        startPos = transform.position;

        Vector2 dirVector = (axis == MovementAxis.Horizontal) ? Vector2.right : Vector2.up;
        if (direction == MoveDirection.Negative)
            dirVector *= -1;

        endPos = startPos + (Vector3)(dirVector.normalized * moveDistance);
        target = endPos;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            target = (target == endPos) ? startPos : endPos;
        }
    }
}
