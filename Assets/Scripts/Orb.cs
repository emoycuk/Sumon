using UnityEngine;

public class Orb : MonoBehaviour
{
    public virtual void ActivateEffect()
    {
        Debug.Log("Orb efekti aktif oldu.");
        Destroy(gameObject); // Orb kaybolsun
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Oyuncu Orb’a dokundu mu?
        {
            ActivateEffect();
        }
    }
}
