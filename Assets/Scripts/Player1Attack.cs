using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    private Animator anim;
    private Player1Movement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<Player1Movement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
{
    // Uncomment the animator trigger if needed
    // anim.SetTrigger("p1attack");
    cooldownTimer = 0;

    int fireballIndex = FindFireball();
    fireballs[fireballIndex].transform.position = firePoint.position;
    fireballs[fireballIndex].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
}

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }


}
