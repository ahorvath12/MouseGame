using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ECM.Controllers;

public class SceneTransition : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        if (GameObject.FindGameObjectWithTag("Player") != null)
            GameObject.FindGameObjectWithTag("Player").GetComponent<BaseCharacterController>().enabled = false;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            anim.SetTrigger("In");
        }
    }

    public void TransitionOut()
    {
        anim.SetTrigger("Out");
    }

    public void TransitionIn()
    {
        anim.SetTrigger("In");
    }

    public void ActivateScene()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<BaseCharacterController>().enabled = true;
    }
    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
