using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player2Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isSlowed = false;
    private bool isInverted = false;

    private Animator anim;
    public int hp = 100;
    public int deathCount = 0;

    public Orb.OrbType lastOrb = Orb.OrbType.Joy;        // Default value (choose what you need)
    public Orb.OrbType secondLastOrb = Orb.OrbType.Joy;

    [Header("Respawn Settings")]
    [Tooltip("The Transform where the player should respawn.")]
    public Transform respawnPoint;

    private bool isInvincible = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float moveInput = 0f;

        // --- ARROW KEYS ONLY ---
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput = -1f;
            transform.localScale = new Vector2(0.7f, 0.7f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveInput = 1f;
            transform.localScale = new Vector2(-0.7f, 0.7f);
        }

        // Apply horizontal velocity
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Jump with UP arrow if grounded
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        //Set animator params
        anim.SetBool("p2walk", moveInput != 0);
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
        if (collision.gameObject.CompareTag("Normal Attack"))
        {
            takeDmg(20);  

            if (isDead())
            {
                Respawn();
            }
        }
        if (collision.gameObject.CompareTag("Slow"))
        {
            isSlowed = true;
        }
        if (collision.gameObject.CompareTag("Invert"))
        {
            isInverted = true;
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
        if (other.CompareTag("DeathZone"))
        {
            Respawn();
        }
        else if (other.CompareTag("SmileOrb") || other.CompareTag("AngerOrb") || other.CompareTag("SadOrb"))
        {
            // Get the Orb component
            Orb orb = other.GetComponent<Orb>();
            if (orb != null)
            {
                // Update our orb history
                UpdateOrbHistory(orb.currentOrbType);
            }
        }
    }

    private void UpdateOrbHistory(Orb.OrbType newOrb)
    {
        // Move the current lastOrb to secondLastOrb
        secondLastOrb = lastOrb;
        // The new orb becomes the lastOrb
        lastOrb = newOrb;

        Debug.Log("2Orbs Updated: Last - " + lastOrb + ", Second Last - " + secondLastOrb);
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
            return;
        }

        hp = hp - dmg;
    }

    public bool isDead()
    {
        return (hp <= 0);
    }
}