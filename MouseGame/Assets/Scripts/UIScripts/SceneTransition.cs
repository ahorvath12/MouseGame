using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ECM.Controllers;

public class SceneTransition : MonoBehaviour
{
    Animator anim;
    public GameObject tutorialPage;
    public bool showTutorialAtStart = false;

    void Awake()
    {
        if (tutorialPage != null)
            ShowTutorial(showTutorialAtStart);
        if (GameObject.FindGameObjectWithTag("Player") != null)
            GameObject.FindGameObjectWithTag("Player").GetComponent<BaseCharacterController>().enabled = false;


        anim = GetComponent<Animator>();
        if (anim != null && SceneManager.GetActiveScene().buildIndex != 1)
        {
            anim.updateMode = AnimatorUpdateMode.UnscaledTime;
            anim.SetTrigger("In");
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
            anim.SetTrigger("Cover");


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
        if (GameObject.FindGameObjectWithTag("Player") != null)
            GameObject.FindGameObjectWithTag("Player").GetComponent<BaseCharacterController>().enabled = true;
    }
    public void ChangeScene(int index)
    {
        Time.timeScale = 1f;
        StartCoroutine(WaitToChangeScene(index));
    }

    public void CanPlayNewMusic()
    {
        MusicManager.Instance.CanChangeMusic();
    }

    public IEnumerator WaitToChangeScene(int index)
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(index);
    }

    public void ShowTutorial(bool show)
    {
        tutorialPage.SetActive(show);
    }

    public void ChangeMusicAfterTransition(AudioClip clip)
    {
        MusicManager.Instance.TransitionMusic(clip);
        //StartCoroutine(MusicManager.Instance.FadeMusicOut(clip));
    }
}
