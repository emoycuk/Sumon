using UnityEngine;

public class Player2Attack : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float attackCooldown = 1f;  // time between attacks

    [Header("References")]
    [SerializeField] private Transform firePoint;        // where the projectile spawns
    [SerializeField] private GameObject[] fireballs;     // array of pooled fireballs

    private Animator anim;
    private Player2Movement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<Player2Movement>();
    }

    private void Update()
    {
        // If you want one shot per click, use GetMouseButtonDown.
        // If you want continuous shooting, use GetMouseButton.
        if (Input.GetKeyDown(KeyCode.O) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        Orb.OrbType lastOrb = playerMovement.lastOrb;
        Orb.OrbType secondLastOrb = playerMovement.secondLastOrb;
        // Optionally trigger an animation
        anim.SetTrigger("p2attack");

        // Reset cooldown
        cooldownTimer = 0f;
        
        // Find an inactive fireball in the pool
        int fireballIndex = FindFireball();
        if (fireballIndex < 0)
        {
            // No available fireball in the pool
            return;
        }

        // Position and activate the fireball
        GameObject fireball = fireballs[fireballIndex];
        float dir = -Mathf.Sign(transform.localScale.x);       // Note the minus sign
        float offsetDistance = 0.5f;
        fireball.transform.position = firePoint.position + Vector3.right * offsetDistance * dir;
        fireball.SetActive(true);

        // Send direction to the projectile (assumes Projectile script handles movement)
        float direction = -Mathf.Sign(transform.localScale.x); // Also invert here
        fireball.GetComponent<Projectile>().SetDirection(direction);
    }

    private int FindFireball()
    {
        // Returns the index of the first inactive fireball in the pool, or -1 if none are available
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return -1;
    }
}