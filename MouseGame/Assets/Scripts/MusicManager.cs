using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    AudioSource audioSource;
    bool canChangeMusic;
    float volume;
    Coroutine coroutine;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        volume = audioSource.volume;

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void ToggleMusic(bool toggle)
    {
        audioSource.enabled = toggle;
    }

    public void CanChangeMusic()
    {
        canChangeMusic = true;
    }

    public void TransitionMusic(AudioClip clip)
    {
        StartCoroutine(FadeMusicOut(clip));
    }

    public IEnumerator FadeMusicOut(AudioClip clip)
    {
        canChangeMusic = false;
        yield return null;
        while (audioSource.volume > 0)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, audioSource.volume - 0.0005f, 5f);
            yield return null;
        }
        audioSource.volume = 0;
        audioSource.Stop();
        audioSource.clip = clip;

        while (!canChangeMusic)
        {
            yield return null;
        }
        audioSource.volume = volume;
        audioSource.Play();
        canChangeMusic = false;
        Debug.Log(audioSource.isPlaying);
    }
}
