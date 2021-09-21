using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject pacStudent;
    public GameObject Ghost1;
    public GameObject Ghost2;
    public GameObject Ghost3;
    public GameObject Ghost4;
    public Sprite[] tiles;

    int[,] levelMap = {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };

    // Start is called before the first frame update
    void Awake()
    {
        pacStudent.transform.position = new Vector3(-20.5f, 12.5f, 0);
        // Testing initial positions
        Ghost1.transform.position = new Vector3(-7.5f, -0.5f, 0); // Ghost 2 Position
        Ghost2.transform.position = new Vector3(-8.5f, -1.5f, 0); // Ghost 3 Position
        Ghost3.transform.position = new Vector3(-7.5f, -1.5f, 0); // Ghost 4 Position
        Ghost4.transform.position = new Vector3(-8.5f, -0.5f, 0); // Ghost 1 Position
        //GetComponent<SpriteRenderer>().sprite = tiles[7];
    }

    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0; i < levelMap.GetLength(0); i++)
        {
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {
                  
                switch (levelMap[i,j])
                {
                    case 0: 
                        break;
                    case 1:
                        Instantiate(tiles[0], new Vector3(-21.5f + i, 13.5f + j, 0), Quaternion.identity);
                        break;
                    case 2: break;
                    case 3: break;
                    case 4: break;
                    case 5: break;
                    case 6: break;
                    case 7: break;

                }
                

                /*
                if(levelMap[i, j] == 1)
                {
                    Instantiate(tiles[0], new Vector3(-21.5f + i, 13.5f + j, 0), Quaternion.identity);
                }
                if(levelMap[i, j] == 2)
                {
                    Instantiate(tiles[1], new Vector3(-21.5f + i, 13.5f + j, 0), Quaternion.identity);
                }
                */

            }
        }
        
    }
}
