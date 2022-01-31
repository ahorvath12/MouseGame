using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FurnitureSpawner : MonoBehaviour
{
    public GameObject[] smallPrefabs, bigPrefabs;
    public LayerMask layerToIgnore;
    public LayerMask spawnLayer;
    public bool respawn = true;

    static int minSpawns = 3, maxSpawns = 5;
    float width, length;
    int localObjs;

    void Awake()
    {
        width = transform.localScale.x / 2;
        length = transform.localScale.z / 2;
        localObjs = 0;
        if (!respawn)
            SpawnPrefabs();
    }

    void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, transform.localScale.x, spawnLayer);
        if (hitColliders.Length < minSpawns)
        {
            SpawnPrefabs();
        }
    }

    // Update is called once per frame
    // void FixedUpdate()
    // {
    //     if (localObjs < minSpawns && respawn)
    //     {
    //         SpawnPrefabs();
    //     }
    // }

    bool PositionRaycast(Vector3 pos, Vector3 scale, Quaternion rot)
    {
        Collider[] hitColliders = new Collider[10];
        int numberOfCollidersFound = Physics.OverlapBoxNonAlloc(pos, scale, hitColliders);
        int numberOfIgnoreCollidersFound = Physics.OverlapBoxNonAlloc(pos, scale / 2, hitColliders, rot, layerToIgnore);

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
            GameObject item = smallPrefabs[Random.Range(0, smallPrefabs.Length)];
            Vector3 spawnPos = new Vector3(transform.position.x + Random.Range(width * -1, width), item.transform.position.y, transform.position.z + Random.Range(length * -1, length));
            Quaternion spawnRot = Quaternion.Euler(0, Random.Range(0, 360), 0);
            if (PositionRaycast(spawnPos, item.GetComponent<NavMeshObstacle>().size, spawnRot))
            {
                Instantiate(item, spawnPos, spawnRot);
                totalToSpawn--;
            }
        }
        if (respawn)
            SpawnOneBigPrefab();
    }

    void SpawnOneBigPrefab()
    {
        GameObject item = bigPrefabs[Random.Range(0, bigPrefabs.Length)];
        Vector3 spawnPos = new Vector3(transform.position.x + Random.Range(width * -1, width), item.transform.position.y, transform.position.z + Random.Range(length * -1, length));
        Quaternion spawnRot = Quaternion.Euler(0, Random.Range(0, 360), 0);
        if (PositionRaycast(spawnPos, item.GetComponent<NavMeshObstacle>().size, spawnRot))
        {
            Instantiate(item, spawnPos, spawnRot);
        }
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag == "Furniture")
    //     {
    //         localObjs++;
    //     }
    // }

    // void OnTriggerExit(Collider other)
    // {
    //     if (other.tag == "Furniture")
    //     {
    //         localObjs--;
    //     }
    // }

}
