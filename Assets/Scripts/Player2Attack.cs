using UnityEngine;

public class Player2Attack : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float attackCooldown = 1f;

    [Header("References")]
    [SerializeField] private Transform firePoint;        
    [SerializeField] private GameObject[] fireballs1;
    [SerializeField] private GameObject[] fireballs2;
    [SerializeField] private GameObject[] fireballs3;

    public bool isPaused = false;

    private Manager manager;
    private Animator anim;
    private Player2Movement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private Orb.OrbType lastOrb;
    private Orb.OrbType secondLastOrb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        manager = FindObjectOfType<Manager>();
        playerMovement = GetComponent<Player2Movement>();
    }

    private void Update()
    {
        if (manager.isPaused)
        {
            return;
        }

        lastOrb = playerMovement.lastOrb;
        secondLastOrb = playerMovement.secondLastOrb;

        if (Input.GetKeyDown(KeyCode.O) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("p2attack");
        cooldownTimer = 0f;

        int fireballIndex = FindFireball();
        if (fireballIndex < 0)
        {
            return;
        }

        int arrayIndex = FindFireballArray();

        GameObject fireball = null;

        if (arrayIndex == 0)
        {
            fireball = fireballs1[fireballIndex];
        }
        else if (arrayIndex == 1)
        {
            fireball = fireballs2[fireballIndex];
        }
        else if (arrayIndex == 2)
        {
            fireball = fireballs3[fireballIndex];
        }
        else
        {
            return;
        }

        float dir = -Mathf.Sign(transform.localScale.x);
        float offsetDistance = 0.5f;
        fireball.transform.position = firePoint.position + Vector3.right * offsetDistance * dir;
        fireball.SetActive(true);

        float direction = -Mathf.Sign(transform.localScale.x);
        fireball.GetComponent<Projectile>().SetDirection(direction);
    }

    private int FindFireballArray()
    {
        if (lastOrb == secondLastOrb)
        {
            return 0;
        }
        else if ((lastOrb == Orb.OrbType.Joy && secondLastOrb == Orb.OrbType.Anger) ||
                 (lastOrb == Orb.OrbType.Anger && secondLastOrb == Orb.OrbType.Joy))
        {
            return 0;
        }
        else if ((lastOrb == Orb.OrbType.Joy && secondLastOrb == Orb.OrbType.Sad) ||
                 (lastOrb == Orb.OrbType.Sad && secondLastOrb == Orb.OrbType.Joy))
        {
            return 1;
        }
        else if ((lastOrb == Orb.OrbType.Anger && secondLastOrb == Orb.OrbType.Sad) ||
                 (lastOrb == Orb.OrbType.Sad && secondLastOrb == Orb.OrbType.Anger))
        {
            return 2;
        }
        return 0;
    }

    private int FindFireball()
    {
        if (lastOrb == secondLastOrb)
        {
            for (int i = 0; i < fireballs1.Length; i++)
            {
                if (!fireballs1[i].activeInHierarchy)
                {
                    return i;
                }
            }
        }
        else if ((lastOrb == Orb.OrbType.Joy && secondLastOrb == Orb.OrbType.Anger) ||
                 (lastOrb == Orb.OrbType.Anger && secondLastOrb == Orb.OrbType.Joy))
        {
            for (int i = 0; i < fireballs1.Length; i++)
            {
                if (!fireballs1[i].activeInHierarchy)
                {
                    return i;
                }
            }
        }
        else if ((lastOrb == Orb.OrbType.Joy && secondLastOrb == Orb.OrbType.Sad) ||
                 (lastOrb == Orb.OrbType.Sad && secondLastOrb == Orb.OrbType.Joy))
        {
            for (int i = 0; i < fireballs2.Length; i++)
            {
                if (!fireballs2[i].activeInHierarchy)
                {
                    return i;
                }
            }
        }
        else if ((lastOrb == Orb.OrbType.Anger && secondLastOrb == Orb.OrbType.Sad) ||
                 (lastOrb == Orb.OrbType.Sad && secondLastOrb == Orb.OrbType.Anger))
        {
            for (int i = 0; i < fireballs3.Length; i++)
            {
                if (!fireballs3[i].activeInHierarchy)
                {
                    return i;
                }
            }
        }
        return -1;
    }
}