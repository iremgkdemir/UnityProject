using UnityEngine;

public class Top : MonoBehaviour
{
    public float speed = 5f;

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
