using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    public GameObject[] orbPrefabs;   // 3 farklı orb prefab
    public Transform[] spawnPoints;   // Spawn noktaları

    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("❌ Spawn noktaları atanmadı!");
            return;
        }

        // (Optional) Force spawn point z=0, in case any Transform is placed at z != 0
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Vector3 tempPos = spawnPoints[i].position;
            spawnPoints[i].position = new Vector3(tempPos.x, tempPos.y, 0f);
        }

        // Spawn the initial orbs
        SpawnOrbs(3);
    }

    private void SpawnOrbs(int orbCount)
    {
        Debug.Log("✅ SpawnOrbs() çağrıldı!");

        for (int i = 0; i < orbCount; i++)
        {
            if (spawnPoints.Length == 0) return;

            // Choose a random spawn point
            Transform spawnPoint = GetRandomSpawnPoint();
            if (spawnPoint == null) return;

            // Choose a random orb type
            int orbTypeIndex = Random.Range(0, orbPrefabs.Length);

            // Instantiate the orb prefab
            GameObject newOrb = Instantiate(orbPrefabs[orbTypeIndex], spawnPoint.position, Quaternion.identity);

            // Force orb's Z position to 0, just in case
            Vector3 fixedPos = newOrb.transform.position;
            fixedPos.z = 0f;
            newOrb.transform.position = fixedPos;

            // Enable the orb
            newOrb.SetActive(true);

            Debug.Log("✅ Yeni Orb spawn oldu: " + newOrb.transform.position);
        }
    }

    // Returns a random spawn point
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