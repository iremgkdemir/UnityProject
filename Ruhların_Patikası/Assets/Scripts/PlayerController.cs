using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private bool facingRight = true;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isJumping;
    private bool isGrounded;
    public bool hasKey = false;
    public bool hasFood = false;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Oyuncu Durumu")]
    public int maxLives = 3;
    private int currentLives;

    [Header("Respawn Noktası")]
    public Transform respawnPoint;
    private Vector3 respawnPosition;

    [Header("Zemin Kontrolü")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Etki Alanı")]
    private bool isNearCat = false;
    public CarCrashController car;

    [Header("Bataklık Ayarları")]
    private bool isInSwamp = false;
    private float swampTimer = 0f;
    public float swampDelay = 2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentLives = maxLives;
        respawnPosition = respawnPoint != null ? respawnPoint.position : transform.position;
    }

    private void Update()
    {
        if (isInSwamp)
        {
            swampTimer += Time.deltaTime;
            rb.linearVelocity = Vector2.zero;

            if (swampTimer >= swampDelay)
            {
                isInSwamp = false;
                swampTimer = 0f;
                Die();
            }

            return;
        }

        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        if (isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = false;
        }

        if (moveInput.x > 0 && !facingRight)
            Flip();
        else if (moveInput.x < 0 && facingRight)
            Flip();

        if (rb.linearVelocity.y < 0)
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else if (rb.linearVelocity.y > 0 && !Keyboard.current.spaceKey.isPressed)
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
            isJumping = true;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && isNearCat)
        {
            car.StartCrash(transform);
            GameManager.Instance.rescuedAnimals++;
        }
    }

    public void Die()
    {
        currentLives--;
        Debug.Log("Can kaybedildi! Kalan: " + currentLives);

        if (currentLives > 0)
            Respawn();
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Respawn()
    {
        transform.position = GameManager.Instance.lastCheckpointPosition;
        rb.linearVelocity = Vector2.zero;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cat")) isNearCat = true;
        if (other.CompareTag("Swamp")) isInSwamp = true;
        if (other.CompareTag("FallZone")) Die();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Cat")) isNearCat = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Hazard"))
        {
            Die();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
