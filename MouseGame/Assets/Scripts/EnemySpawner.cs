using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public int minSpawns = 4, maxSpawns = 8;
    public LayerMask layerToIgnore;

    float width, length;
    int localEnemies = 0;

    void Awake()
    {
        width = transform.localScale.x / 2;
        length = transform.localScale.z / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(name + ": " + localEnemies);
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

        while (totalToSpawn > 0)
        {
            Vector3 spawnPos = new Vector3(transform.position.x + Random.Range(width * -1, width), 0, transform.position.z + Random.Range(length * -1, length));
            if (PositionRaycast(spawnPos))
            {
                Instantiate(prefabs[Random.Range(0, prefabs.Length)], spawnPos, Quaternion.identity);
                totalToSpawn--;
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && other.name.Contains("ECM"))
        {
            localEnemies++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy" && other.name.Contains("ECM"))
        {
            localEnemies--;
        }
    }
}
