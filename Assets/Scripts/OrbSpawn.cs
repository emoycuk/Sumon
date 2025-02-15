using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    public GameObject[] orbPrefabs; // Sad, Joy, Anger prefabları
    public Transform[] spawnPoints; // Tileset noktaları
    public float spawnInterval = 5f; // 5 saniyede bir spawn

    void Start()
    {
        StartCoroutine(SpawnOrbs());
    }

    IEnumerator SpawnOrbs()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            
            // Rastgele bir tileset noktası seç
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Rastgele bir Orb türü seç
            GameObject randomOrb = orbPrefabs[Random.Range(0, orbPrefabs.Length)];

            // Orb'u sahneye yerleştir
            Instantiate(randomOrb, randomSpawnPoint.position, Quaternion.identity);
        }
    }
}

