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
    public Text gameoverText;
    public PacStudentController characterController;
    public GhostController ghostController;
    private bool ghostTimerActive;
    public Text ghostTimerText;
    private float ghostTimer = 10;
    private TimeSpan ghostTimePlaying;

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Start()
    {
        timerText.text = "00:00:00";
        gameoverText.text = "Game Over!";
        gameoverText.enabled = false; 
        timerActive = false;
        //StartTimer();
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<PacStudentController>();
        ghostController = GameObject.FindGameObjectWithTag("GhostManager").GetComponent<GhostController>();
        characterController.enabled = false;
        ghostController.enabled = false;
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
        characterController.enabled = true;
        ghostController.enabled = true;
        //gameoverText.enabled = true;

        yield return new WaitForSeconds(1.0f);

        countdownText.gameObject.SetActive(false);

    }

    public void GhostTimerCountdown()
    {
        ghostTimerActive = true;
        StartCoroutine(StartGhostCountdown());
    }

    private IEnumerator StartGhostCountdown()
    {
        //yield return null; 
        while (ghostTimerActive)
        {
            if (ghostTimer > 0)
            {
                ghostTimer -= Time.deltaTime;
                ghostTimePlaying = TimeSpan.FromSeconds(ghostTimer);
                string ghostTimeStr = ghostTimePlaying.ToString("mm':'ss':'ff");
                ghostTimerText.text = ghostTimeStr;

                if (ghostTimer < 3)
                {
                    // Recovering State
                    ghostController.RecoverState();
                }

            }
            else
            {
                ghostTimer = 0;
                ghostController.GhostAnimation();
                ghostController.scaredMusic.Stop();
            }

            yield return null;
        }
    }

    public void GameOver()
    {
        gameoverText.enabled = true;
        characterController.enabled = false;
        ghostController.enabled = false;
    }


}
