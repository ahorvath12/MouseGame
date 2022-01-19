using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ECM.Controllers;

public class PlayerManager : MonoBehaviour
{

    [SerializeField]
    private int walkSpeed = 5, runSpeed = 10;
    public DetectObject cheeseDetector;
    public Animator anim;

    BaseCharacterController charController;

    bool canEatCheese = false;


    public UnityEvent OnEatCheeseEvent;

    void Start()
    {
        charController = GetComponent<BaseCharacterController>();
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            //sprint
            charController._speed = runSpeed;
            anim.SetFloat("Speed", 1.5f);
        }
        else
        {
            charController._speed = walkSpeed;
            anim.SetFloat("Speed", 1);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //pause
            Time.timeScale = 0;
        }
        else
            Time.timeScale = 1f;

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            //eat cheese
            if (canEatCheese && cheeseDetector.ObjectInRange != null)
            {
                Destroy(cheeseDetector.ObjectInRange);
                OnEatCheeseEvent?.Invoke();
            }
        }
    }

    public void IsCheeseInRange(bool inRange)
    {
        canEatCheese = inRange;
    }

}
