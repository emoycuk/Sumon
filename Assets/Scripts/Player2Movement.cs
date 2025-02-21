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

    private Manager manager;
    private Animator anim;

    public int hp = 100;
    public int deathCount = 0;

    public Orb.OrbType lastOrb = Orb.OrbType.Joy; 
    public Orb.OrbType secondLastOrb = Orb.OrbType.Joy;

    [Header("Respawn Settings")]
    [Tooltip("The Transform where the player should respawn.")]
    public Transform respawnPoint;

    private bool isInvincible = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<Manager>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (manager.isPaused)
        {
            return;
        }

        float moveInput = 0f;

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

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        anim.SetBool("p2walk", moveInput != 0);
        if (isDead())
        {
            Respawn();
        }
    }

    // COLLISION CHECKS
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
            Orb orb = other.GetComponent<Orb>();
            if (orb != null)
            {
                UpdateOrbHistory(orb.currentOrbType);
            }
        }
    }

    private void UpdateOrbHistory(Orb.OrbType newOrb)
    {
        secondLastOrb = lastOrb;
        lastOrb = newOrb;
    }

    private void Respawn()
    {
        transform.position = respawnPoint.position;
        hp = 100;
        isGrounded = true;

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