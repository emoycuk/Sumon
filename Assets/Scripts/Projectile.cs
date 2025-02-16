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
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")) return; // Ignore ground
        if (collision.CompareTag("Attack")) return; // Ignore other attacks
        if (collision.CompareTag("AngerOrb")) return;
        if (collision.CompareTag("SmileOrb")) return;
        if (collision.CompareTag("SadOrb")) return;

        if (collision.CompareTag("Player1")) // Check for player collision
        {
            Player1Movement player = collision.GetComponent<Player1Movement>();
            if (player != null)
            {
                player.takeDmg(dmg);  // Deal damage
            }
        }
        if (collision.CompareTag("Player2")) // Check for player collision
        {
            Player2Movement player = collision.GetComponent<Player2Movement>();
            if (player != null)
            {
                player.takeDmg(dmg);  // Deal damage
            }
        }

        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");
        Deactivate();
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0;
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