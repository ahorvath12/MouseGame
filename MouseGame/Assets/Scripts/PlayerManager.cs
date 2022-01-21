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
    public UnityEvent OnEatCheeseEvent;

    Rigidbody rbody;
    UIManager uiManager;

    BaseCharacterController charController;

    bool canEatCheese = false;
    float currentStamina = 100;
    Coroutine regenStamina;


    void Awake()
    {
        uiManager = UIManager.Instance;
        charController = GetComponent<BaseCharacterController>();
        rbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (cheeseDetector.ObjectInRange == null)
        {
            anim.SetBool("Eat", false);
            charController.enabled = true;
        }

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && currentStamina > 0)
        {
            //sprint
            charController._speed = runSpeed;
            anim.SetFloat("Speed", 1.5f);
            if (rbody.velocity != Vector3.zero)
                UseStamina(20 * Time.deltaTime);
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
                anim.SetBool("Eat", true);
                charController.enabled = false;
            }

        }

    }

    public void IsCheeseInRange(bool inRange)
    {
        canEatCheese = inRange;
    }


    void UseStamina(float amount)
    {
        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            uiManager.SetSliderVal(currentStamina);
            //staminaBar.value = currentStamina;

            if (regenStamina != null)
                StopCoroutine(regenStamina);

            regenStamina = StartCoroutine(RegenStamina());
        }
        else
        {
            anim.SetFloat("Speed", 1f);
            charController._speed = walkSpeed;
        }
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(1);

        while (currentStamina < 100)
        {
            //currentStamina += 2f;
            currentStamina = Mathf.Lerp(currentStamina, currentStamina + 1, 0.1f);
            uiManager.SetSliderVal(currentStamina);
            yield return null;
        }
        regenStamina = null;
    }
}
