using UnityEngine;

public class SpiritFollower : MonoBehaviour
{
    public Transform target;               
    public Vector2 followOffset = new Vector2(0f, 2f);  
    public float followSpeed = 3f;         

    void Update()
    {
        if (target == null) return;

        Vector2 targetPosition = (Vector2)target.position + followOffset;
        transform.position = Vector2.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
