using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    /*
    public GameObject pacStudent;
    public GameObject Ghost1;
    public GameObject Ghost2;
    public GameObject Ghost3;
    public GameObject Ghost4;
    public GameObject ManualLevel;
    */
    //public GameObject PowerPellet;
    private bool powerPelletAdded = false;
    public GameObject[] levelElements;
    private int elementLeft;
    

    
    int[,] levelMap = {
        // Top-Left and Right
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7, 7,2,2,2,2,2,2,2,2,2,2,2,2,1},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4, 4,5,5,5,5,5,5,5,5,5,5,5,5,2},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4, 4,5,3,4,4,4,3,5,3,4,4,3,5,2},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4, 4,5,4,0,0,0,4,5,4,0,0,4,6,2},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3, 3,5,3,4,4,4,3,5,3,4,4,3,5,2},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5, 5,5,5,5,5,5,5,5,5,5,5,5,5,2},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4, 4,4,4,3,5,3,3,5,3,4,4,3,5,2},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3, 3,4,4,3,5,4,4,5,3,4,4,3,5,2},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4, 4,5,5,5,5,4,4,5,5,5,5,5,5,2},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4, 4,0,3,4,4,3,4,5,1,2,2,2,2,1},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3, 3,0,3,4,4,3,4,5,2,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0, 0,0,0,0,0,4,4,5,2,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0, 0,4,4,3,0,4,4,5,2,0,0,0,0,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0, 0,0,0,4,0,3,3,5,1,2,2,2,2,2},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0, 0,0,0,4,0,0,0,5,0,0,0,0,0,0},
        // Bottom-Left 
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0, 0,0,0,4,0,0,0,5,0,0,0,0,0,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0, 0,0,0,4,0,3,3,5,1,2,2,2,2,2},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0, 0,4,4,3,0,4,4,5,2,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0, 0,0,0,0,0,4,4,5,2,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3, 3,0,3,4,4,3,4,5,2,0,0,0,0,0},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4, 4,0,3,4,4,3,4,5,1,2,2,2,2,1},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4, 4,5,5,5,5,4,4,5,5,5,5,5,5,2},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3, 3,4,4,3,5,4,4,5,3,4,4,3,5,2},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4, 4,4,4,3,5,3,3,5,3,4,4,3,5,2},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5, 5,5,5,5,5,5,5,5,5,5,5,5,5,2},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3, 3,5,3,4,4,4,3,5,3,4,4,3,5,2},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4, 4,5,4,0,0,0,4,5,4,0,0,4,6,2},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4, 4,5,3,4,4,4,3,5,3,4,4,3,5,2},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4, 4,5,5,5,5,5,5,5,5,5,5,5,5,2},
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7, 7,2,2,2,2,2,2,2,2,2,2,2,2,1},
        //{0,0,0,0,0,0,5,0,0,0,4,0,0,0, 0},
        //{2,2,2,2,2,1,5,3,3,0,4,0,0,0, 0},
        //{0,0,0,0,0,2,5,4,4,0,3,4,4,0, 0},
    };
    
    /*
    int[,] levelMap =
     {
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
    */

    // Start is called before the first frame update
    void Awake()
    {
        /*
        Destroy(ManualLevel);
        pacStudent.transform.position = new Vector3(-12.5f, 13.5f, 0);
        // Testing initial positions
        //Ghost1.transform.position = new Vector3(0.5f, 0.5f, 0); // Ghost 2 Original Position
        //Ghost2.transform.position = new Vector3(-0.5f, -0.5f, 0); // Ghost 3 Original Position
        //Ghost3.transform.position = new Vector3(0.5f, -0.5f, 0); // Ghost 4 Original Position
        //Ghost4.transform.position = new Vector3(-0.5f, 0.5f, 0); // Ghost 1 Original Position
        Ghost1.transform.position = new Vector3(-0.5f, 0.5f, 0); 
        Ghost2.transform.position = new Vector3(0.5f, 0.5f, 0); 
        Ghost3.transform.position = new Vector3(-0.5f, -0.5f, 0); 
        Ghost4.transform.position = new Vector3(0.5f, -0.5f, 0); 
         */                                                        //PowerPellet.transform.position = new Vector3(-13.5f + 1, 14.5f - 3, 0);

        // This is for Case 6 Power Pellet 
        for (int m = 0; m < levelMap.GetLength(0); m++)
        {
            for (int n = 0; n < levelMap.GetLength(1); n++)
            {
                if (levelMap[m, n] == 6)
                {
                    Instantiate(levelElements[5], new Vector3(-13.5f + n, 14.5f - m, 0), Quaternion.identity);
                }
            }
        }
    }

    private void Start()
    {
        //Instantiate(levelElements[5], new Vector3(-13.5f + 1, 14.5f - 3, 0), Quaternion.identity);
        /*
        // This is for Case 6 Power Pellet 
        for (int m = 0; m < levelMap.GetLength(0); m++)
        {
            for (int n = 0; n < levelMap.GetLength(1); n++)
            {
                if (levelMap[m,n] == 6)
                {
                    Instantiate(levelElements[5], new Vector3(-13.5f + n, 14.5f - m, 0), Quaternion.identity);
                }
            }
        } 
        */

    }

    // Update is called once per frame
    void Update()
    //void FixedUpdate()
    {
        
        for (int i = 0; i < levelMap.GetLength(0); i++)
        {
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {

                //while (i >= 0 && j >= 0)
                //{
                
                if (i >= 0 && j >= 0)
                {

                    switch (levelMap[i, j])
                    {
                        case 0: // Empty
                            elementLeft = levelMap[i, j];
                            break;

                        case 1: // Outside Corner
                            if (elementLeft == 2 && i > 0 && levelMap[i - 1, j] == 2) // OC 4 and OC 2 (but need to make a second one for that) 
                            {
                                if (j < levelMap.GetLength(1) / 2 && i > 11 && i < 25)
                                {
                                    Instantiate(levelElements[0], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 180));
                                    elementLeft = levelMap[i, j];
                                }
                                else if (j < levelMap.GetLength(1) / 2 && i < levelMap.GetLength(0) / 2)
                                {
                                    Instantiate(levelElements[0], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 90));
                                    elementLeft = levelMap[i, j];
                                }
                                else  if (j < levelMap.GetLength(1) / 2 & i > levelMap.GetLength(0) / 2)
                                {
                                    Instantiate(levelElements[0], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 90));
                                    elementLeft = levelMap[i, j];
                                }
                                else
                                {
                                    Instantiate(levelElements[0], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 180));
                                    elementLeft = levelMap[i, j];
                                }
                            }
                            
                            else if (i > 0 && levelMap[i - 1, j] == 2 && levelMap[i+1,j] == 0) // OC 4 (on the right) 
                            {

                                Instantiate(levelElements[0], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 90));
                                elementLeft = levelMap[i, j];
                            }
                            else if (i > 0 && elementLeft == 2 && levelMap[i-1, j] == 5 && levelMap[i+1,j] == 2) // OC 3
                            {
                                Instantiate(levelElements[0], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 270));
                                elementLeft = levelMap[i, j];
                            }

                            else if (j > levelMap.GetLength(1) / 2 && elementLeft == 2) // End Corner Row 0 
                            {
                                Instantiate(levelElements[0], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 180, 0));
                                elementLeft = levelMap[i, j];
                            }
                            else if (i > levelMap.GetLength(0) / 2 && levelMap[i-1,j] == 0 && elementLeft == 2) // OC 1 (Bottom Left) 
                            {
                                Instantiate(levelElements[0], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 180, 0));
                                elementLeft = levelMap[i, j];
                            }
                            else if (i > levelMap.GetLength(0) / 2 && elementLeft == 5 && levelMap[i-1,j] == 2)
                            {
                                Instantiate(levelElements[0], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 90));
                                elementLeft = levelMap[i, j];
                            }
                            else
                            {
                                Instantiate(levelElements[0], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.identity);
                                elementLeft = levelMap[i, j];
                            }
                            break;

                        case 2: // Outside Wall
                            if (i > 0 && levelMap[i-1, j] == 1) // Tile above
                            {
                                Instantiate(levelElements[1], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 90));
                                elementLeft = levelMap[i, j];
                            }
                            else if (i > 0 && levelMap[i - 1, j] == 2) // Tile above
                            {
                                Instantiate(levelElements[1], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 90));
                                elementLeft = levelMap[i, j];
                            }
                            else
                            {
                                Instantiate(levelElements[1], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.identity);
                                elementLeft = levelMap[i, j];
                            }
                            break;

                        case 3: // Inside Corner
                            if (elementLeft == 4)
                            {
                                if (levelMap[i - 1, j] == 4 && levelMap[i + 1, j] == 5)
                                {
                                    Instantiate(levelElements[2], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 180)); // 90 works for T points, 180 for big blocks
                                    elementLeft = levelMap[i, j];
                                }
                                else if (levelMap[i + 1, j] == 4 && levelMap[i - 1, j] == 3) // for bottom corner of T section 
                                {
                                    if (j > levelMap.GetLength(1) / 2) // Right Side Bottom Corner of T Section
                                    {
                                        Instantiate(levelElements[2], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 270));
                                        elementLeft = levelMap[i, j];
                                    }
                                    else
                                    {
                                        Instantiate(levelElements[2], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 0));
                                        elementLeft = levelMap[i, j];
                                    }
                                }
                                else if (levelMap[i + 1, j] == 4 && levelMap[i - 1, j] == 4)  // for bottom corner of T section
                                {
                                    if (j < levelMap.GetLength(1) / 2 && i > levelMap.GetLength(0) / 2)
                                    {
                                        Instantiate(levelElements[2], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 180, 90));
                                        elementLeft = levelMap[i, j];
                                    }
                                    else
                                    {
                                        Instantiate(levelElements[2], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 180, 0));
                                        elementLeft = levelMap[i, j];
                                    }
                                }
                                else if (levelMap[i - 1, j] == 4) // Top Corner of Side T
                                {
                                    if (j > levelMap.GetLength(1) / 2)
                                    {
                                        Instantiate(levelElements[2], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 180)); // 90 works for T points, 180 for big blocks
                                        elementLeft = levelMap[i, j];
                                    }
                                    else if (j > levelMap.GetLength(1) / 2 && i > levelMap.GetLength(0) / 2)
                                    {
                                        Instantiate(levelElements[2], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 180)); // 90 works for T points, 180 for big blocks
                                        elementLeft = levelMap[i, j];
                                    }
                                    else
                                    {
                                        Instantiate(levelElements[2], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 90)); // 90 works for T points, 180 for big blocks
                                        elementLeft = levelMap[i, j];
                                    }
                                }
                                else if (levelMap[i - 1, j] == 3) // Bottom Corner of Side T
                                {
                                    Instantiate(levelElements[2], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 180));
                                    elementLeft = levelMap[i, j];
                                }
                                else
                                {
                                    Instantiate(levelElements[2], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 180, 0));
                                    elementLeft = levelMap[i, j];
                                }
                            }
                            
                            else if (elementLeft == 3)
                            {
                                if (levelMap[i - 1, j] == 4 && levelMap[i + 1, j] == 4)
                                {
                                    if (j > levelMap.GetLength(1) / 2 - 1 && i > levelMap.GetLength(0) / 2 + 1) // Bottom Side Corner on Bottom T 
                                    {
                                        Instantiate(levelElements[2], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 90));
                                        elementLeft = levelMap[i, j];
                                    }
                                    else
                                    {
                                        Instantiate(levelElements[2], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 180, 270));
                                        elementLeft = levelMap[i, j];
                                    }
                                }
                                else if (levelMap[i - 1, j] == 4)
                                {
                                    Instantiate(levelElements[2], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 180));
                                    elementLeft = levelMap[i, j];
                                }
                                else // Connected Corners
                                {
                                    Instantiate(levelElements[2], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 180, 0));
                                    elementLeft = levelMap[i, j];
                                }
                            }
                            
                            else if (levelMap[i - 1, j] == 4)
                            {
                                Instantiate(levelElements[2], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 90));
                                elementLeft = levelMap[i, j];
                            }
                            else if (levelMap[i-1, j] == 3 && levelMap[i, j+1] == 4)
                            {
                                Instantiate(levelElements[2], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 180, 180));
                                elementLeft = levelMap[i, j];
                            }
                            else
                            {
                                Instantiate(levelElements[2], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.identity);
                                elementLeft = levelMap[i, j];
                            }
                            break;

                        case 4: // Inside Wall
                            if (elementLeft == 3)
                            {
                                if (j > levelMap.GetLength(1) / 2 && levelMap[i-1,j] == 4 && levelMap[i+1,j] == 4 || j > 14 && levelMap[i - 1, j] == 4 && levelMap[i + 1, j] == 3)
                                {
                                    Instantiate(levelElements[3], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 90));
                                    elementLeft = levelMap[i, j];
                                }
                                else 
                                {
                                    Instantiate(levelElements[3], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 0));
                                    elementLeft = levelMap[i, j];
                                }
                            }
                            else if (elementLeft == 4 && levelMap[i + 1, j] == 0 || elementLeft == 4 && levelMap[i + 1, j] == 5)
                            {
                                Instantiate(levelElements[3], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 0));
                                elementLeft = levelMap[i, j];
                            }
                            else if (i > 0 && levelMap[i - 1, j] == 3)
                            {
                                Instantiate(levelElements[3], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 90));
                                elementLeft = levelMap[i, j];
                            }
                            else if (i > 0 && levelMap[i - 1, j] == 4)
                            {
                                Instantiate(levelElements[3], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 90));
                                elementLeft = levelMap[i, j];
                            }
                            else if (i > 0 && levelMap[i - 1, j] == 7)
                            {
                                Instantiate(levelElements[3], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 90));
                                elementLeft = levelMap[i, j];
                            }
                            else if (elementLeft == 4 && i > 0 && levelMap[i+1, j] != 4)
                            {
                                Instantiate(levelElements[3], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 0));
                                elementLeft = levelMap[i, j];
                            }
                            
                            else
                            {
                                Instantiate(levelElements[3], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.identity);
                                elementLeft = levelMap[i, j];
                            } 
                            break;

                        case 5: // Normal Pellet
                            Instantiate(levelElements[4], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.identity);
                            //Instantiate(levelElements[0], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.identity);
                            elementLeft = levelMap[i, j];
                            break;

                        case 6: // Power Pellet
                            elementLeft = levelMap[i, j];
                            break;

                        case 7: // T-Junction
                            if (elementLeft == 2)
                            {
                                if (i > levelMap.GetLength(0) / 2)
                                {
                                    Instantiate(levelElements[6], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 0, 180));
                                    elementLeft = levelMap[i, j];
                                }
                                else
                                {
                                    Instantiate(levelElements[6], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 180, 0));
                                    elementLeft = levelMap[i, j];
                                }
                            }
                            else
                            {
                                if (i > levelMap.GetLength(0)/2)
                                {
                                    Instantiate(levelElements[6], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.Euler(0, 180, 180));
                                    elementLeft = levelMap[i, j];
                                }
                                else
                                {
                                    Instantiate(levelElements[6], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.identity);
                                    elementLeft = levelMap[i, j];
                                }
                            }
                            break;

                    }
                }

                //Instantiate(LevelManager, new Vector3(0.5f, 14.5f, 0f), Quaternion.Euler(0, 180, 0));

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
                //}

            }
        }
        
    }

    void PowerPelletAnimation()
    {
        //powerPelletAdded = false;
        //Instantiate(levelElements[5], position, Quaternion.identity);
        if (!powerPelletAdded)
        {
            //Instantiate(levelElements[5], position, Quaternion.identity);
            //Instantiate(levelElements[5], new Vector3(-13.5f + j, 14.5f - i, 0), Quaternion.identity);
            //elementLeft = levelMap[i, j];
            powerPelletAdded = true;
            //PPCount++;
        }
                
       
    }

}
