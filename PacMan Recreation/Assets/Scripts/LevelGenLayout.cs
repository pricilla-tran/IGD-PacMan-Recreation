using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenLayout : MonoBehaviour
{
    public GameObject LevelManager; // Parent Object 
    private GameObject newQuadarant;
    public List<Transform> initialPositions;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(LevelManager, new Vector3(-13.5f, 14.5f, 0), Quaternion.identity);
        //Instantiate(LevelManager, new Vector3(0.5f, 14.5f, 0f), Quaternion.Euler(0, 180, 45));
        //newQuadarant.transform.parent = LevelManager.transform;
        List<Transform> initialPositions = new List<Transform>();

        GameObject quad_1 = Instantiate(LevelManager, new Vector3(-13.5f, 14.5f, 0), Quaternion.identity);
        GameObject quad_2 = Instantiate(LevelManager, new Vector3(13.5f, 14.5f, 0f), Quaternion.Euler(0, 0, 45));

        //initialPositions.Add(quad_1.transform);
        //initialPositions.Add(quad_2.transform);



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
