using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        exitButton.onClick.AddListener(ExitLoad);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ExitLoad()
    {
        SceneManager.LoadScene("StartScene");
    }
}
