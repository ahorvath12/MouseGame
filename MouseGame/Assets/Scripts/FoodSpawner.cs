using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public float offsetY = 0.37f;
    public bool respawn, despawn;
    public int maxSpawns = 15;
    public int minSpawns = 7;
    public LayerMask layerToIgnore;
    int existingFood = 0;

    float width, length;

    void Awake()
    {
        width = transform.localScale.x / 2;
        length = transform.localScale.z / 2;
    }

    void Start()
    {
        SpawnPrefabs();
    }

    void FixedUpdate()
    {
        if (existingFood < minSpawns && respawn)
        {
            SpawnPrefabs();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Food")
        {
            existingFood++;
            if (despawn && existingFood > maxSpawns)
            {
                Destroy(other.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Food")
        {
            existingFood--;
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
        int total = maxSpawns - existingFood;
        while (total > 0)
        {
            Vector3 spawnPos = new Vector3(transform.position.x + Random.Range(width * -1, width), offsetY, transform.position.z + Random.Range(length * -1, length));
            if (PositionRaycast(spawnPos))
            {
                Instantiate(prefabs[Random.Range(0, prefabs.Length)], spawnPos, Quaternion.identity);
                total--;
            }
        }
    }
}
