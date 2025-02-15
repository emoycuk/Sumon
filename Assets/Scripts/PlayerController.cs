using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Varsayılan hız
    public float jumpHeight = 2f; // Varsayılan zıplama yüksekliği

    public void ModifySpeed(float amount)
    {
        moveSpeed += amount;
        Debug.Log("Yeni hız: " + moveSpeed);
    }

    public void ModifyJump(float amount)
    {
        jumpHeight += amount;
        Debug.Log("Yeni zıplama yüksekliği: " + jumpHeight);
    }
}
