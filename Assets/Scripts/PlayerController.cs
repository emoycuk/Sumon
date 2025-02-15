using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  
    public float jumpForce = 5f;  
    private Rigidbody2D rb;
    private bool isGrounded;

    public int sadOrbCount = 0;
    public int joyOrbCount = 0;
    public int angerOrbCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y); 

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void ModifySpeed(float amount)
    {
        moveSpeed += amount;
        moveSpeed = Mathf.Clamp(moveSpeed, 1f, 10f);
        Debug.Log("⚡ Yeni hız: " + moveSpeed);
    }

    public void ModifyJump(float amount)
    {
        jumpForce += amount;
        jumpForce = Mathf.Clamp(jumpForce, 2f, 10f);
        Debug.Log("🆙 Yeni zıplama kuvveti: " + jumpForce);
    }

    public void CollectOrb(string orbType)
    {
        if (orbType == "Sad")
        {
            sadOrbCount++;
            Debug.Log("😢 Sad Orb Sayısı: " + sadOrbCount);
        }
        else if (orbType == "Joy")
        {
            joyOrbCount++;
            Debug.Log("😊 Joy Orb Sayısı: " + joyOrbCount);
        }
        else if (orbType == "Anger")
        {
            angerOrbCount++;
            Debug.Log("😡 Anger Orb Sayısı: " + angerOrbCount);
        }
    }
}
