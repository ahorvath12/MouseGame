using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

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
}
