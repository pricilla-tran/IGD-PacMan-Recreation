using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button lvl1Button;
    public Button lvl2Button;

    // Start is called before the first frame update
    void Start()
    {
        lvl1Button.onClick.AddListener(Level1Load);
        lvl2Button.onClick.AddListener(Level2Load);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Level1Load()
    {
        SceneManager.LoadScene("LevelScene");
    }

    private void Level2Load()
    {
        
    }

}
