using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM.Controllers;

public class PlayerManager : MonoBehaviour
{

    [SerializeField]
    private int walkSpeed = 5, runSpeed = 10;
    public DetectObject cheeseDetector;

    BaseCharacterController charController;

    bool canEatCheese = false;


    void Start()
    {
        charController = GetComponent<BaseCharacterController>();
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //sprint
            charController._speed = runSpeed;
        }
        else
            charController._speed = walkSpeed;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //pause
            Time.timeScale = 0;
        }
        else
            Time.timeScale = 1f;

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            if (canEatCheese && cheeseDetector.ObjectInRange != null)
            {
                Destroy(cheeseDetector.ObjectInRange);
            }
        }
    }

    public void IsCheeseInRange(bool inRange)
    {
        canEatCheese = inRange;
    }

}
