using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;
    public int dmg;

    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore certain tags
        if (collision.CompareTag("Ground")) return;
        if (collision.CompareTag("AngerOrb")) return;
        if (collision.CompareTag("SmileOrb")) return;
        if (collision.CompareTag("SadOrb")) return;

        bool shouldExplode = false;

        if (collision.CompareTag("Player1"))
        {
            Player1Movement player = collision.GetComponent<Player1Movement>();
            if (player != null)
            {
                player.takeDmg(dmg);
                shouldExplode = true;
            }
        }
        if (collision.CompareTag("Player2"))
        {
            Player2Movement player = collision.GetComponent<Player2Movement>();
            if (player != null)
            {
                player.takeDmg(dmg);
                shouldExplode = true;
            }
        }
        if (collision.CompareTag("NormalAttack"))
        {
            shouldExplode = true;
        }
        if (collision.CompareTag("Slow"))
        {
            shouldExplode = true;
        }
        if (collision.CompareTag("Invert"))
        {
            shouldExplode = true;
        }

        if (shouldExplode)
        {
            anim.SetTrigger("explode");
            hit = true;
            boxCollider.enabled = false;
            Invoke("Deactivate", 0.5f);
        }
        else
        {
            hit = true;
            boxCollider.enabled = false;
            Deactivate();
        }
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0f;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
