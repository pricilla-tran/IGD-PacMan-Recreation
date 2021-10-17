using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static int highScore;
    private static int currentScore;
    public Text highScoreText;
    public Text highScoreTimeText;
    private float startTime;
    private float minutes;
    private float seconds;
    private float milliseconds;

    // Start is called before the first frame update
    void Start()
    {
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        currentScore = PlayerPrefs.GetInt("Score", 0);
        startTime = Time.time;
        minutes = Mathf.Floor(startTime / 60);
        seconds = Mathf.Floor(startTime % 60);
        milliseconds = Mathf.Floor(startTime * 6f);

        highScoreTimeText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
    }

    // Update is called once per frame
    void Update()
    {

    }


    public static int HighScoreKeeper
    {
        get
        {
            return highScore;
        }
    }

    public static int CurrentScoreKeeper
    {
        get
        {
            return currentScore;
        }
        
    }


    public static void LoadScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = (currentScore > PlayerPrefs.GetInt("HighScore") ? currentScore : PlayerPrefs.GetInt("HighScore"));
        }
    }
}
