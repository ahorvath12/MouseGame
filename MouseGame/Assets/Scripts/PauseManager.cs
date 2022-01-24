using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseScreen;
    public Image musicIcon;
    public Sprite[] musicIcons;
    int iconNumber = 0;
    bool paused = false;

    void Awake()
    {
        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //pause
            if (!paused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }

    public void Pause()
    {
        if (!paused)
        {
            paused = true;
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
        }
    }

    public void Unpause()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToggleMusic()
    {
        switch (iconNumber)
        {
            case 0:
                iconNumber = 1;
                MusicManager.Instance.ToggleMusic(false);
                break;
            case 1:
                iconNumber = 0;
                MusicManager.Instance.ToggleMusic(true);
                break;
        }
        musicIcon.sprite = musicIcons[iconNumber];
    }
}
