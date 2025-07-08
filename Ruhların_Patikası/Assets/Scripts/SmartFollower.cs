using UnityEngine;

public class SmartFollower : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 3f;
    public Vector2 followOffset = new Vector2(0, 2f); 

    private bool isFollowing = false;

    public void StartFollowing(Transform newTarget)
    {
        target = newTarget;
        isFollowing = true;

        Vector3 flyPosition = target.position + (Vector3)followOffset;
        transform.position = flyPosition;
    }

    void Update()
    {
        if (!isFollowing || target == null) return;

        Vector2 desiredPosition = (Vector2)target.position + followOffset;
        transform.position = Vector2.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
}
