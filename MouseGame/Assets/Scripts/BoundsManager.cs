using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsManager : MonoBehaviour
{
    private Material wallMat;
    private bool collided = false;

    void Start()
    {
        wallMat = transform.parent.GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (collided && wallMat.color.a < 1)
        {
            //fade in
            Color c = wallMat.color;
            c.a = Mathf.Lerp(c.a, 1, 0.01f);
            wallMat.color = c;
        }
        else if (!collided && wallMat.color.a > 0)
        {
            //fade out
            Color c = wallMat.color;
            c.a = Mathf.Lerp(c.a, 0, 0.01f);
            wallMat.color = c;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            collided = true;
    }


    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            collided = false;
    }
}
