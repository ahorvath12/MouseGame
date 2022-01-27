using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI cheeseCounterText, timerText;
    public Slider slider1, slider2;

    int cheeseCounter = 0;
    float timeSeconds, timeMinutes, timeHours;
    string timer = "";

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        timerText.text = ConvertFloatToTimer();
    }

    public void UpdateCheeseCounter()
    {
        cheeseCounter++;
        cheeseCounterText.text = cheeseCounter.ToString();
    }

    string ConvertFloatToTimer()
    {
        string time = "";

        int currentTime = Mathf.RoundToInt(Time.time);

        //if timer is below a minute
        if (currentTime < 60)
        {
            time = "00:";
            if (currentTime < 10)
                time += "0";
            return time + currentTime;
        }

        //add seconds
        int seconds = currentTime % 60;
        if (seconds < 10)
            time = "0" + seconds;
        else
            time += seconds;

        time = ":" + time;

        //add minutes to the time string
        int minutes = (int)(currentTime / 60f);
        if (minutes < 10)
            time = "0" + minutes + time;
        else
            time = minutes + time;

        if (minutes < 60)
            return time;

        time = ":" + time;

        //add hours to the time string
        int hours = minutes / 60;
        time = hours + time;
        if (hours < 10)
            time = "0" + time;


        return time;
    }

    public void SetSliderVal(float val)
    {
        slider1.value = val;
        slider2.value = val;
    }

    public void PlaySqueak(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
    }
}
