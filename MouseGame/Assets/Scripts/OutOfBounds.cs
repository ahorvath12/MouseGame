using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.tag == "Food" || other.tag == "Enemy" || other.tag == "Furniture")
        {
            Destroy(other.gameObject);
            return;
        }

        //hot fix for destroying the van
        if (other.name.Contains("car-hippie-van"))
        {
            if (other.transform.parent != null)
            {

                Destroy(other.transform.parent.gameObject);
                return;
            }
            Destroy(other.gameObject);
        }
    }
}
