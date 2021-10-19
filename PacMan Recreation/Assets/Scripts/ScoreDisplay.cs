using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text currentScore;
    public Text currentScoreTime;
    private float startTime;
    private float minutes;
    private float seconds;
    private float milliseconds;
    private PacStudentController characterController;

    // Start is called before the first frame update
    void Start()
    {
        currentScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        //startTime = Time.time;
        //minutes = Mathf.Floor(startTime / 60);
        //seconds = Mathf.Floor(startTime % 60);
        //milliseconds = Mathf.Floor(startTime * 6f);
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<PacStudentController>();

        //currentScoreTime.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
    }

    // Update is called once per frame
    void Update()
    {
        currentScore.text = characterController.score.ToString();
    }
}
