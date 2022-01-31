using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ECM.Controllers;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField]
    private int walkSpeed = 5, runSpeed = 10;
    public DetectObject cheeseDetector;
    public Animator anim;
    public AudioClip[] gameOverSounds;
    public UnityEvent OnEatCheeseEvent;
    [HideInInspector] public bool caught;

    Rigidbody rbody;
    UIManager uiManager;
    AudioSource audioSource;
    BaseCharacterController charController;

    bool canEatCheese = false;
    float currentStamina = 100;
    Coroutine regenStamina;


    void Awake()
    {
        if (Instance == null || Instance != this)
            Instance = this;

        uiManager = UIManager.Instance;
        charController = GetComponent<BaseCharacterController>();
        rbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (cheeseDetector.ObjectInRange == null)
        {
            anim.SetBool("Eat", false);
            //charController.enabled = true;
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



        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            //eat cheese
            if (cheeseDetector.ObjectInRange != null)
            {
                if (cheeseDetector.ObjectInRange.transform.parent != null)
                    Destroy(cheeseDetector.ObjectInRange.transform.parent);
                else Destroy(cheeseDetector.ObjectInRange);
                OnEatCheeseEvent?.Invoke();
                anim.SetBool("Eat", true);
                //charController.enabled = false;
            }

        }

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
            currentStamina = Mathf.Lerp(currentStamina, currentStamina + 5, 3 * Time.deltaTime);
            uiManager.SetSliderVal(currentStamina);
            yield return null;
        }
        regenStamina = null;
    }

    public void Caught()
    {
        charController.enabled = false;
        caught = true;
        rbody.velocity = Vector3.zero;
        anim.SetBool("Death", true);
        StartCoroutine(PlayNoiseOnLoop());
        StartCoroutine(UIManager.Instance.SlideInGameOverScreen());
    }

    IEnumerator PlayNoiseOnLoop()
    {
        float timePassed = 0;
        while (caught)
        {
            timePassed += Time.deltaTime;
            if (!audioSource.isPlaying && timePassed > Random.Range(0, 4))
            {
                audioSource.clip = gameOverSounds[Random.Range(0, gameOverSounds.Length)];
                audioSource.Play();
                timePassed = 0;
            }
            yield return null;
        }

        while (audioSource.volume > 0)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, audioSource.volume - 0.05f, 3f * Time.deltaTime);
            yield return null;
        }
        audioSource.volume = 0;
        audioSource.Stop();
        this.enabled = false;
    }

}
