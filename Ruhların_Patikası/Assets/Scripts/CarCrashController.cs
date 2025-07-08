using UnityEngine;

public class CarCrashController : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;
    private bool moveToTarget = false;

    public FadeEffect fadeEffect; 

    private void Update()
    {
        if (moveToTarget && target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, target.position) < 0.1f)
            {
                fadeEffect.StartFadeOut("Sahne1_YuvayaDoðru"); 
                moveToTarget = false;
            }
        }
    }

    public void StartCrash(Transform playerTransform)
    {
        target = playerTransform;
        moveToTarget = true;
    }
}
