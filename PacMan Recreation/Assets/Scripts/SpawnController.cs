using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject spawnGate;
    public GameObject Ghost1;
    public GameObject Ghost2;
    public GameObject Ghost3;
    public GameObject Ghost4;
    private bool Ghost1Leave = false;
    private bool Ghost2Leave = false;
    private bool Ghost3Leave = false;
    private bool Ghost4Leave = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ghost" && Ghost1Leave || collision.tag == "Ghost" && Ghost2Leave || collision.tag == "Ghost" && Ghost3Leave || collision.tag == "Ghost" && Ghost4Leave)
        {
            Debug.Log("Return");
        }
        else if (collision.tag == "Ghost")
        {
            if (!Ghost1Leave)
            {
                Ghost1Leave = true;
                Debug.Log("Ghost1 Leaving");
            }
            else if (!Ghost2Leave)
            {
                Ghost2Leave = true;
                Debug.Log("Ghost2 Leaving");
            }
            else if (!Ghost3Leave)
            {
                Ghost3Leave = true;
                Debug.Log("Ghost3 Leaving");
            }
            else if(!Ghost4Leave)
            {
                Ghost4Leave = true;
                Debug.Log("Ghost4 Leaving");
            }
        }
    }
}
