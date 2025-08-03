using UnityEngine;

public class CrushingWall : MonoBehaviour
{
    public enum MovementDirection { UpToDown, DownToUp }
    public MovementDirection direction = MovementDirection.UpToDown;

    public float moveDistance = 3f;
    public float moveSpeed = 2f;
    public float waitTime = 1f;

    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public float crushDetectionHeight = 0.05f;
    private float lastDamageTime = -999f;
    public float damageCooldown = 1f; // Saniyede 1 kez hasar verebilir


    private Vector3 startPos;
    private Vector3 targetPos;
    private bool movingForward = true;
    private float waitTimer = 0f;
    private BoxCollider2D box;

    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        startPos = transform.position;
        Vector3 offset = direction == MovementDirection.UpToDown ? Vector3.down : Vector3.up;
        targetPos = startPos + offset * moveDistance;
    }

    void Update()
    {
        if (waitTimer > 0f)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        Vector3 target = movingForward ? targetPos : startPos;
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            movingForward = !movingForward;
            waitTimer = waitTime;
        }

        DetectCrushing();
    }

    void DetectCrushing()
    {
        Bounds wallBounds = box.bounds;

        Vector2 checkCenter = direction == MovementDirection.UpToDown
            ? new Vector2(wallBounds.center.x, wallBounds.min.y - crushDetectionHeight / 2f)
            : new Vector2(wallBounds.center.x, wallBounds.max.y + crushDetectionHeight / 2f);

        Vector2 checkSize = new Vector2(wallBounds.size.x * 0.9f, crushDetectionHeight);

        Collider2D player = Physics2D.OverlapBox(checkCenter, checkSize, 0f, playerLayer);
        Collider2D ground = Physics2D.OverlapBox(checkCenter, checkSize, 0f, groundLayer);

        // Cooldown kontrolü: sadece belirli aralıklarla hasar ver
        if (player != null && ground != null && Time.time > lastDamageTime + damageCooldown)
        {
            PlayerController pc = player.GetComponent<PlayerController>();
            if (pc != null)
            {
                Debug.Log("Sıkıştı, can gitti!");
                pc.Die(); // Canı azalt
                lastDamageTime = Time.time; // Yeni hasar zamanı kaydedilir
            }
        }
    }


    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;

        Bounds wallBounds = GetComponent<BoxCollider2D>().bounds;

        Vector2 checkCenter = direction == MovementDirection.UpToDown
            ? new Vector2(wallBounds.center.x, wallBounds.min.y - crushDetectionHeight / 2f)
            : new Vector2(wallBounds.center.x, wallBounds.max.y + crushDetectionHeight / 2f);

        Vector2 checkSize = new Vector2(wallBounds.size.x * 0.9f, crushDetectionHeight);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(checkCenter, checkSize);
    }
}
