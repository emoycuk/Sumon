using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    public GameObject[] orbPrefabs; // 3 farklı orb prefab
    public Transform[] spawnPoints; // Spawn noktaları

    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("❌ Spawn noktaları atanmadı!");
            return;
        }

        SpawnOrbs(3); // İlk başta 3 orb spawn et
    }

    void SpawnOrbs(int orbCount)
    {
        Debug.Log("✅ SpawnOrbs() çağrıldı!"); 

        for (int i = 0; i < orbCount; i++)
        {
            if (spawnPoints.Length == 0) return;

            Transform spawnPoint = GetRandomSpawnPoint();

            if (spawnPoint == null) return;

            int orbTypeIndex = Random.Range(0, orbPrefabs.Length);
            GameObject newOrb = Instantiate(orbPrefabs[orbTypeIndex], spawnPoint.position, Quaternion.identity);
            newOrb.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0);
            newOrb.SetActive(true);
            
            Debug.Log("✅ Yeni Orb spawn oldu: " + newOrb.transform.position);
        }
    }

    public Transform GetRandomSpawnPoint()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("❌ Spawn noktaları atanmadı!");
            return null;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex];
    }
}
