using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] Rigidbody rbody;
    Animator anim;
    Coroutine randomCooldown;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        rbody = transform.parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rbody.velocity != Vector3.zero)
        {
            anim.SetBool("Run", true);
            anim.SetBool("Idle", false);
        }
        else
        {
            anim.SetBool("Run", false);
            anim.SetBool("Idle", true);

            if (gameObject.tag == "Enemy")
            {
                if (anim.GetInteger("RandomAction") != 0)
                    anim.SetInteger("RandomAction", 0);
                if (randomCooldown == null)
                    randomCooldown = StartCoroutine(RandomAnimCoolDown());
            }
        }
    }

    IEnumerator RandomAnimCoolDown()
    {
        yield return new WaitForSeconds(Random.Range(3, 7));

        if (Random.Range(0, 100) > 50)
        {
            Debug.Log("random anim");
            anim.SetInteger("RandomAction", Random.Range(1, 4));
        }

        //anim.SetInteger("RandomAction", 0);
        randomCooldown = null;
    }

    public void ResetRandomAnim()
    {
        anim.SetInteger("RandomAction", 0);
    }
}
