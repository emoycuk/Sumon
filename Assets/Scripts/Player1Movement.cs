using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player1Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private Animator anim;

    public int hp = 100;
    public int deathCount = 0;

    public Orb orb1;
    public Orb orb2;

    [Header("Respawn Settings")]
    [Tooltip("The Transform where the player should respawn.")]
    public Transform respawnPoint;

    private bool isInvincible = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator
        anim = GetComponent<Animator>();
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

        // Jump with W if grounded
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        //Set animator params
        anim.SetBool("walk", moveInput != 0);

        if (isDead())
        {
            Respawn();
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
        if (collision.gameObject.CompareTag("Attack"))
        {
            takeDmg(20);  // Adjust damage as needed
                              // Optional: Check if the player is dead and handle accordingly
            if (isDead())
            {
                Respawn();
            }
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
        hp = 100;
        isGrounded = true;

        // Reset health (if desired) when respawning
        // hp = 100; 

        deathCount++;
    }

    public bool canAttack()
    {
        return true;
    }

    public void takeDmg(int dmg)
    {
        if (dmg > hp)
        {
            hp = 0;
        }
        else
        {
            hp = hp - dmg;
        }
    }

    public bool isDead()
    {
        return (hp <= 0);
    }
}