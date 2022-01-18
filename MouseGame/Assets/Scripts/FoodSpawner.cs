using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject[] prefabs;

    public int maxSpawns = 15;
    public int minSpawns = 7;
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

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (existingFood < minSpawns)
            {
                Debug.Log("spawning food");
                SpawnPrefabs();
            }
        }
    }

    void SpawnPrefabs()
    {
        int total = maxSpawns - existingFood;
        Debug.Log(total);
        while (total > 0)
        {
            Vector3 spawnPos = new Vector3(transform.position.x + Random.Range(width * -1, width), 0, transform.position.z + Random.Range(length * -1, length));
            Instantiate(prefabs[Random.Range(0, prefabs.Length)], spawnPos, Quaternion.identity);
            total--;
            existingFood++;
        }

    }
}
