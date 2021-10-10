using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static TimerManager instance;
    public Text timerText;
    private bool timerActive;
    private TimeSpan timePlaying;
    private float timer;
    public int countdownTime;
    public Text countdownText;

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Start()
    {
        timerText.text = "00:00:00";
        timerActive = false;
        //StartTimer();
        StartCoroutine(Countdown());
    }

    public void StartTimer()
    {
        timerActive = true;
        timer = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerActive = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerActive)
        {
            timer += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(timer);
            string timeActiveStr = timePlaying.ToString("mm':'ss':'ff");
            timerText.text = timeActiveStr;

            yield return null;
        }
        
    }

    private IEnumerator Countdown()
    {
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();

            yield return new WaitForSeconds(1.0f);

            countdownTime--;
        }

        countdownText.text = "GO!";
        StartTimer();

        yield return new WaitForSeconds(1.0f);

        countdownText.gameObject.SetActive(false);

    }


}