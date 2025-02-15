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
        
        if (other.CompareTag("Player1"))
        {
            Debug.Log("✅ " + currentOrbType + " Orb alındı!");

            StartCoroutine(RespawnOrb()); 
        }
        if (other.CompareTag("Player2"))
        {
            Debug.Log("✅ " + currentOrbType + " Orb alındı!");

            StartCoroutine(RespawnOrb());
        }
    }

    IEnumerator RespawnOrb()
{
    Debug.Log("⏳ Orb kayboluyor ve 5 saniye bekliyor...");

    // Hide the current orb before respawning
    GetComponent<SpriteRenderer>().enabled = false;
    GetComponent<Collider2D>().enabled = false; // Optional: Disable collision to prevent interactions

    yield return new WaitForSeconds(5f); // Wait for 5 seconds before respawning

    if (spawner != null)
    {
        // Get a new random spawn point
        Transform newSpawnPoint = spawner.GetRandomSpawnPoint(); 

        // Pick a new random orb prefab
        GameObject newOrbPrefab = spawner.orbPrefabs[Random.Range(0, spawner.orbPrefabs.Length)];

        // Destroy the current orb and spawn a new one
        GameObject newOrb = Instantiate(newOrbPrefab, newSpawnPoint.position, Quaternion.identity);

        // Set the correct orb type
        Orb newOrbScript = newOrb.GetComponent<Orb>();
        currentOrbType = newOrbScript.currentOrbType;

        Debug.Log("🔄 Yeni Orb şu noktada spawn oldu: " + newSpawnPoint.position + " Type: " + currentOrbType);

        // Destroy the old orb (this script is on the old orb)
        Destroy(gameObject);
    }
}
}
