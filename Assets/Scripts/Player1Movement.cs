using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player1Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private bool isGrounded = false;

    [Header("Respawn Settings")]
    [Tooltip("The Transform where the player should respawn.")]
    public Transform respawnPoint;

    private bool isInvincible = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = 0f;

        // --- ARROW KEYS ONLY ---
        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f;
            transform.localScale = new Vector2(-0.7f, 0.7f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f;
            transform.localScale = new Vector2(0.7f, 0.7f);
        }

        // Apply horizontal velocity
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Jump with UP arrow if grounded
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // BASIC COLLISION CHECKS
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            Respawn();
            isGrounded = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if we've collided with the DeathZone
        if (other.CompareTag("DeathZone"))
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        // Move the player to the respawn position
        transform.position = respawnPoint.position;
    }

    public bool canAttack()
    {
        return true;
    }
}