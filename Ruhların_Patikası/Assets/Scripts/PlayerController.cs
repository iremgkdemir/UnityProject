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

    [Header("Oyuncu Durumu")]
    public int maxLives = 3;
    private int currentLives;

    [Header("Respawn Noktası")]
    public Transform respawnPoint; // Bu objeyi Inspector'da elle atayacağız
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

        if (respawnPoint != null)
            respawnPosition = respawnPoint.position;
        else
            respawnPosition = transform.position;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void Respawn()
    {
        transform.position = respawnPosition;
        rb.linearVelocity = Vector2.zero; // hızı sıfırla
    }

    private void Update()
    {
        if (isInSwamp)
        {
            swampTimer += Time.deltaTime;
            rb.linearVelocity = Vector2.zero; // bataklıkta hareketsiz kal

            if (swampTimer >= swampDelay)
            {
                isInSwamp = false;
                swampTimer = 0f;
                Die(); // can kaybı
            }

            return; // diğer kodları durdur
        }

        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        if (isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = false;
        }

        if (moveInput.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput.x < 0 && facingRight)
        {
            Flip();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            isJumping = true;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && isNearCat)
        {
            Debug.Log("Kediye yardım edildi!");
            car.StartCrash(transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cat"))
        {
            isNearCat = true;
            Debug.Log("Kediye yaklaşıldı");
        }

        if (other.CompareTag("Swamp"))
        {
            if (!isInSwamp)
            {
                isInSwamp = true;
                swampTimer = 0f;
                Debug.Log("Bataklığa düştün! Hareket edemiyorsun.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Cat"))
        {
            isNearCat = false;
            Debug.Log("Kediden uzaklaşıldı");
        }
    }

    private void Die()
    {
        currentLives--;
        Debug.Log("Can kaybedildi! Kalan: " + currentLives);

        if (currentLives > 0)
        {
            Respawn(); // checkpoint'e dön
        }
        else
        {
            Debug.Log("Oyun bitti!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // sahneyi baştan yükle
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
