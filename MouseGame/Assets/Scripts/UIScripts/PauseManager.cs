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
    public AudioClip squeak1, squeak2;

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
            UIManager.Instance.PlaySqueak(squeak1);
            pauseScreen.SetActive(true);
            Cursor.visible = true;
        }
    }

    public void Unpause()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1f;
            UIManager.Instance.PlaySqueak(squeak2);
            pauseScreen.SetActive(false);
            Cursor.visible = false;
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToggleMusic()
    {
        switch (MusicManager.Instance.audioSource.isPlaying)
        {
            case true:
                iconNumber = 1;
                MusicManager.Instance.ToggleMusic(false);
                break;
            case false:
                iconNumber = 0;
                MusicManager.Instance.ToggleMusic(true);
                break;
        }
        musicIcon.sprite = musicIcons[iconNumber];
    }
}
