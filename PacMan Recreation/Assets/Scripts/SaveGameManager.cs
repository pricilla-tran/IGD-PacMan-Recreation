using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.GetInt("HighScore");
        PlayerPrefs.GetString("Time");
    }

    // Update is called once per frame
    void Update()
    {
        SaveSpeed();
    }

    public void SaveSpeed()
    {
        PlayerPrefs.SetInt("HighScore", ScoreManager.HighScoreKeeper);
        PlayerPrefs.Save();
    }

}
