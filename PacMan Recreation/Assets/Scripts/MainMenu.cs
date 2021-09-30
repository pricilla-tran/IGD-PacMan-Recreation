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
    public GameObject[] pizza;
    private float timer;
    private float moveWait = 1.5f;
    private float moveCycle;

    // Start is called before the first frame update
    void Start()
    {
        lvl1Button.onClick.AddListener(Level1Load);
        lvl2Button.onClick.AddListener(Level2Load);
        pizza[0].transform.position = new Vector3(-10, 8, 0);
        pizza[1].transform.position = new Vector3(10, -8, 0); 
    }

    // Update is called once per frame
    void Update()
    {
        BorderMovement();
    }

    void BorderMovement()
    {
        timer += Time.deltaTime;
        if ((int)timer == (int)moveWait && moveCycle == 0)
        {
            timer = 0;
            pizza[0].transform.position += Vector3.down * 16;
            pizza[1].transform.position += Vector3.up * 16;
            moveCycle++;
        }
        else if ((int)timer == (int)moveWait && moveCycle == 1)
        {
            timer = 0;
            pizza[0].transform.position += Vector3.right * 20;
            pizza[1].transform.position += Vector3.left * 20;
            moveCycle++;
        }
        else if ((int)timer == (int)moveWait && moveCycle == 2)
        {
            timer = 0;
            pizza[0].transform.position += Vector3.up * 16;
            pizza[1].transform.position += Vector3.down * 16;
            moveCycle++;
        }
        else if ((int)timer == (int)moveWait && moveCycle == 3)
        {
            timer = 0;
            pizza[0].transform.position += Vector3.left * 20;
            pizza[1].transform.position += Vector3.right * 20;
            moveCycle = 0;
        }
    }

    void Level1Load()
    {
        SceneManager.LoadScene("LevelScene");
    }

    private void Level2Load()
    {
        
    }

}
