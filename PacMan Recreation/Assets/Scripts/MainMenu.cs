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
    private Tweener BGTweener;

    // Start is called before the first frame update
    void Start()
    {
        lvl1Button.onClick.AddListener(Level1Load);
        lvl2Button.onClick.AddListener(Level2Load);
        pizza[0].transform.position = new Vector3(0, 0, 0);
        //BGTweener = gameObject.GetComponent<Tweener>();
        
        //pizza[1].transform.position = new Vector3(10, -8, 0); 
    }

    // Update is called once per frame
    void Update()
    {
        BorderMovement();
    }

    void BorderMovement()
    {
        
        timer += Time.deltaTime;
        /*
        if ((int)timer == (int)moveWait && moveCycle == 0)
        {
            //timer = 0;
            //pizza[0].transform.position += Vector3.down * 16;
            //pizza[1].transform.position += Vector3.up * 16;
            pizza[0].transform.position = Vector3.Lerp( pizza[0].transform.position, Vector3.right * 2, 0.5f);
            moveCycle++;
        }
        else if ((int)timer == (int)moveWait && moveCycle == 1)
        {
            timer = 0;
            //pizza[0].transform.position += Vector3.right * 16;
            //pizza[1].transform.position += Vector3.left * 20;
            //pizza[0].transform.position = Vector3.Lerp(pizza[0].transform.position, Vector3.left * 2, 0.5f);
            moveCycle = 0;
        }
        */

    }

    void Level1Load()
    {
        SceneManager.LoadScene("LevelScene");
    }

    private void Level2Load()
    {
        
    }

}
