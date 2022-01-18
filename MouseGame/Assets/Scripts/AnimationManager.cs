using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] Rigidbody rbody;
    Animator anim;
    bool setRandomAnim = false;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        rbody = transform.parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rbody.velocity != Vector3.zero)
        {
            anim.SetBool("Run", true);
            anim.SetBool("Idle", false);
        }
        else
        {
            setRandomAnim = false;
            anim.SetBool("Run", false);
            anim.SetBool("Idle", true);
            if (gameObject.tag == "Enemy")
            {
                if (Random.Range(0, 100) > 75 && !setRandomAnim)
                {
                    anim.SetInteger("RandomAction", Random.Range(1, 4));
                    setRandomAnim = true;
                }
            }
        }
    }
}
