using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI cheeseCounterText, timerText, endTimer, endCheese;
    public Slider slider1, slider2;
    public GameObject gameOverScreen;
    [HideInInspector] public bool running = false;

    int cheeseCounter = 0;
    float timeSeconds, timeMinutes, timeHours;
    string timer = "";
    int currentTime = 0;
    float actualTime = 0;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (running)
            timerText.text = ConvertFloatToTimer();
        if (slider1.value != slider2.value)
            slider2.value = slider1.value;
    }

    public void UpdateCheeseCounter()
    {
        cheeseCounter++;
        cheeseCounterText.text = cheeseCounter.ToString();
    }

    string ConvertFloatToTimer()
    {
        string time = "";

        //int currentTime = Mathf.RoundToInt(Time.time);
        actualTime += Time.deltaTime;
        currentTime = (int)actualTime;

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
    }

    public void PlaySqueak(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
    }

    public IEnumerator SlideInGameOverScreen()
    {
        running = false;
        yield return new WaitForSeconds(1f);

        PlayerManager.Instance.caught = false;
        endTimer.text = timerText.text;
        endCheese.text = cheeseCounter.ToString();

        Vector3 newPos = new Vector3(gameOverScreen.transform.localPosition.x, 0, gameOverScreen.transform.localPosition.z);

        while (gameOverScreen.transform.localPosition.y > 0)
        {
            float yVal = Mathf.Lerp(gameOverScreen.transform.position.y, gameOverScreen.transform.position.y - 5f, 3 * Time.deltaTime);
            gameOverScreen.transform.position = new Vector3(gameOverScreen.transform.position.x, yVal, gameOverScreen.transform.position.z);
            yield return null;
        }

        gameOverScreen.transform.localPosition = newPos;
    }
}
