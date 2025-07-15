using UnityEngine;

public class CarCrashController : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;
    private bool moveToTarget = false;

    public FadeEffect fadeEffect;

    private bool hasFaded = false; 

    private void Update()
    {
        if (moveToTarget && target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            if (!hasFaded && Vector2.Distance(transform.position, target.position) < 1.7f)
            {
                fadeEffect.StartFadeOut("Sahne1_YuvayaDoğru");
                hasFaded = true;
                moveToTarget = false;
            }
        }
    }

    public void StartCrash(Transform playerTransform)
    {
        target = playerTransform;
        moveToTarget = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasFaded && other.CompareTag("Player"))
        {
            fadeEffect.StartFadeOut("Sahne1_YuvayaDoğru");
            hasFaded = true;
            moveToTarget = false;
        }
    }

}
