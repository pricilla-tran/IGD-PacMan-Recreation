using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text highScore;
    public Text highScoreTime;
    private float startTime;
    private float minutes;
    private float seconds;
    private float milliseconds;

    // Start is called before the first frame update
    void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        startTime = Time.time;
        minutes = Mathf.Floor(startTime / 60);
        seconds = Mathf.Floor(startTime % 60);
        milliseconds = Mathf.Floor(startTime * 6f);

        highScoreTime.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
