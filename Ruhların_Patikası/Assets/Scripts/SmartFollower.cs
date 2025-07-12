using UnityEngine;

public class SpiritFollower : MonoBehaviour
{
    public Transform player;
    public Vector3 offsetRight = new Vector3(-1.5f, 1.2f, 0f);
    public Vector3 offsetLeft = new Vector3(1.5f, 1.2f, 0f);
    public float followSpeed = 5f;

    void Update()
    {
        bool isFacingRight = player.localScale.x > 0;
        Vector3 targetPos = player.position + (isFacingRight ? offsetRight : offsetLeft);
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
    }
}
