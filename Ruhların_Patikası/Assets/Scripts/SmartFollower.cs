using UnityEngine;

public class SpiritFollower : MonoBehaviour
{
    public Transform player;
    public Vector3 offsetRight = new Vector3(-1.5f, 1.2f, 0f);
    public Vector3 offsetLeft = new Vector3(1.5f, 1.2f, 0f);
    public float followSpeed = 5f;
    public LayerMask obstacleLayer;
    public float avoidDistance = 0.5f; 
    private void Update()
    {
        bool isFacingRight = player.localScale.x > 0;
        Vector3 targetPos = player.position + (isFacingRight ? offsetRight : offsetLeft);

        RaycastHit2D hit = Physics2D.Linecast(transform.position, targetPos, obstacleLayer);

        if (hit.collider == null)
        {
            MoveTo(targetPos);
        }
        else
        {
            Vector3 rightStep = transform.position + new Vector3(avoidDistance, 0, 0);
            Vector3 leftStep = transform.position - new Vector3(avoidDistance, 0, 0);

            bool canGoRight = !Physics2D.Linecast(transform.position, rightStep, obstacleLayer);
            bool canGoLeft = !Physics2D.Linecast(transform.position, leftStep, obstacleLayer);

            if (canGoRight)
                MoveTo(rightStep);
            else if (canGoLeft)
                MoveTo(leftStep);
        }
    }

    private void MoveTo(Vector3 target)
    {
        transform.position = Vector3.Lerp(transform.position, target, followSpeed * Time.deltaTime);
    }
}
