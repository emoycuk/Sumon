using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    public GameObject[] orbPrefabs;   // 3 different orb prefabs
    public Transform[] spawnPoints;   // Spawn points

    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            return;
        }

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Vector3 tempPos = spawnPoints[i].position;
            spawnPoints[i].position = new Vector3(tempPos.x, tempPos.y, 0f);
        }

        // Spawn the initial orbs
        SpawnOrbs(8);
    }

    private void SpawnOrbs(int orbCount)
    {
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

            Vector3 fixedPos = newOrb.transform.position;
            fixedPos.z = 0f;
            newOrb.transform.position = fixedPos;

            newOrb.SetActive(true);
        }
    }

    // Returns a random spawn point
    public Transform GetRandomSpawnPoint()
    {
        if (spawnPoints.Length == 0)
        {
            return null;
        }
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex];
    }
}