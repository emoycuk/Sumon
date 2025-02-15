using UnityEngine;
using System.Collections;

public class Orb : MonoBehaviour
{
    public enum OrbType { Sad, Joy, Anger } 
    public OrbType currentOrbType; 
    private OrbSpawner spawner;

    private void Start()
    {
        spawner = FindObjectOfType<OrbSpawner>(); // Spawner'ı bul
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.CollectOrb(currentOrbType.ToString());
            }

            Debug.Log("❌ " + currentOrbType + " Orb alındı!"); 
            StartCoroutine(RespawnOrb()); 
        }
    }

    IEnumerator RespawnOrb()
    {
        gameObject.SetActive(false); // Orbu kaybolmuş gibi yap
        yield return new WaitForSeconds(5f); // 5 saniye bekle

        if (spawner != null)
        {
            Transform newSpawnPoint = spawner.GetRandomSpawnPoint(); // Rastgele spawn noktası al
            transform.position = newSpawnPoint.position; // Orbu yeni noktaya taşı
            Debug.Log("🔄 Yeni Orb şu noktada spawn oldu: " + transform.position);
        }
        
        gameObject.SetActive(true); // Orbu tekrar görünür yap
    }
}
