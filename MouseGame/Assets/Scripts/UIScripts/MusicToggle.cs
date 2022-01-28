using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    public Image musicIcon;
    public Sprite[] musicIcons;
    int iconNumber = 0;

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
