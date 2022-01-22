using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Food" || other.tag == "Enemy" || other.tag == "Furniture")
        {
            Destroy(other.gameObject);
        }
    }
}
