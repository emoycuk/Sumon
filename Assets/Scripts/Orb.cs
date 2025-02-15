using UnityEngine;
using System.Collections;
using UnityEditor.Overlays;

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
        Debug.Log("Trigger Çalıştı! " + other.name); // **Bu çalışıyor mu kontrol et**
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("✅ " + currentOrbType + " Orb alındı!");

            StartCoroutine(RespawnOrb()); 
        }
    }

    IEnumerator RespawnOrb()
    {
        Debug.Log("⏳ Orb kayboluyor ve 5 saniye bekliyor...");
        GetComponent<SpriteRenderer>().enabled = false;
        
        if (spawner != null)
        {
            Transform newSpawnPoint = spawner.GetRandomSpawnPoint(); // Yeni spawn noktcurrentOrbType = spawner.orbPrefabs[Random.Range(0, spawner.orbPrefabs.Length)];ası al
            GameObject newOrbPrefab = spawner.orbPrefabs[Random.Range(0, spawner.orbPrefabs.Length)];
            Orb newOrbScript = newOrbPrefab.GetComponent<Orb>();
            currentOrbType = newOrbScript.currentOrbType;
            transform.position = newSpawnPoint.position; // Orb’u yeni konuma taşı
            Debug.Log("🔄 Yeni Orb şu noktada spawn oldu: " + transform.position + currentOrbType);
        }

        yield return new WaitForSeconds(5f); // 5 saniye bekle
        GetComponent<SpriteRenderer>().enabled = true; 
        Debug.Log("✅ Orb tekrar aktif oldu!");
    }
}
