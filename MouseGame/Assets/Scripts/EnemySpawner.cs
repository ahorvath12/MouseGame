using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public int minSpawns, maxSpawns;
    public LayerMask layerToIgnore;

    float width, length;
    int localEnemies = 0;

    void Awake()
    {
        width = transform.localScale.x / 2;
        length = transform.localScale.z / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (localEnemies < minSpawns)
        {
            SpawnPrefabs();
        }
    }

    bool PositionRaycast(Vector3 pos)
    {
        float overlapTestSize = 3;
        Collider[] hitColliders = new Collider[10];
        int numberOfCollidersFound = Physics.OverlapSphereNonAlloc(pos, overlapTestSize, hitColliders);
        int numberOfIgnoreCollidersFound = Physics.OverlapSphereNonAlloc(pos, overlapTestSize, hitColliders, layerToIgnore);

        if (numberOfCollidersFound - numberOfIgnoreCollidersFound == 0)
        {
            return true;
        }
        return false;
    }

    void SpawnPrefabs()
    {
        int totalToSpawn = Random.Range(minSpawns, maxSpawns + 1);

        while (localEnemies < totalToSpawn)
        {
            Vector3 spawnPos = new Vector3(transform.position.x + Random.Range(width * -1, width), 0, transform.position.z + Random.Range(length * -1, length));
            if (PositionRaycast(spawnPos))
            {
                Instantiate(prefabs[Random.Range(0, prefabs.Length)], spawnPos, Quaternion.identity);
                localEnemies++;
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            localEnemies++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            localEnemies--;
        }
    }
}
