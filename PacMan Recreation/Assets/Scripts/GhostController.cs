using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public GameObject Ghost1;
    public GameObject Ghost2;
    public GameObject Ghost3;
    public GameObject Ghost4;
    private Animator Ghost1Animator;
    private Animator Ghost2Animator;
    private Animator Ghost3Animator;
    private Animator Ghost4Animator;
    private Tweener ghost1Tweener;
    private Tweener ghost2Tweener;
    private Tweener ghost3Tweener;
    private Tweener ghost4Tweener;
    private Vector3[] ghost4Pos;
    public float moveSpeed = 2;
    private int cycleCount = 0;
    private Vector3 dirG1Vector;
    private Vector3 dirG2Vector;
    private Vector3 dirG3Vector;
    private Vector3 dirG4Vector;
    private Vector3 scaredDirVector;
    public AudioSource scaredMusic;
    public GameObject pacStudent;
    private PacStudentController psController;
    public bool scaredStateActive = false;
    private Vector3[] spawnPoints;
    private bool setScared1 = false;
    private bool setScared2 = false;
    private bool setScared3 = false;
    private bool setScared4 = false;
    public AudioSource deathMusic;
    private bool deathStateActive = false;

    // Start is called before the first frame update
    void Start()
    {
        ghost4Pos = new Vector3[4];
        ghost4Pos[0] = new Vector3(0.5f, 3.5f, 0);
        ghost4Pos[1] = new Vector3(4.5f, 3.5f, 0);
        ghost4Pos[2] = new Vector3(4.5f, -0.5f, 0);
        ghost4Pos[3] = new Vector3(7.5f, -0.5f, 0);

        dirG1Vector = Vector3.up;
        dirG2Vector = Vector3.up;
        dirG3Vector = Vector3.up;
        dirG4Vector = Vector3.up;
        scaredDirVector = Vector3.up;

        psController = pacStudent.GetComponent<PacStudentController>();

        Ghost1Animator = Ghost1.GetComponent<Animator>();
        Ghost2Animator = Ghost2.GetComponent<Animator>();
        Ghost3Animator = Ghost3.GetComponent<Animator>();
        Ghost4Animator = Ghost4.GetComponent<Animator>();

        ghost1Tweener = Ghost1.GetComponent<Tweener>();
        ghost2Tweener = Ghost2.GetComponent<Tweener>();
        ghost3Tweener = Ghost3.GetComponent<Tweener>();
        ghost4Tweener = Ghost4.GetComponent<Tweener>();

        spawnPoints = new Vector3[2];
        spawnPoints[0] = new Vector3(-0.5f, 2.5f, 0f);
        spawnPoints[1] = new Vector3(-0.5f, 2.5f, 0f);

        //GhostAnimation();

        //MoveG3Direction();

    }

    // Update is called once per frame
    void Update()
    {
        if (!scaredStateActive && !deathStateActive)
        {
            Ghost1Move();
            Ghost1Animation();
            Ghost2Move();
            Ghost2Animation();
            Ghost3Move();
            Ghost3Animation();
            Ghost4Move();
            Ghost4Animation();
            setScared1 = false;
            setScared2 = false;
            setScared3 = false;
            setScared4 = false;
            deathMusic.Stop();
        }
        else if (scaredStateActive)
        {
            
            ScaredMove(Ghost1);
            ScaredMove(Ghost2);
            ScaredMove(Ghost3);
            ScaredMove(Ghost4);
            /*
            setScared1 = true;
            setScared2 = true;
            setScared3 = true;
            setScared4 = true;
            */
            //ScaredState();
        }
        
    }

    public void ScaredState()
    {
        Ghost1Animator.ResetTrigger("Down");
        Ghost1Animator.ResetTrigger("Up");
        Ghost1Animator.ResetTrigger("Left");
        Ghost1Animator.ResetTrigger("Right");
        Ghost1Animator.ResetTrigger("Recover");

        Ghost2Animator.ResetTrigger("Down");
        Ghost2Animator.ResetTrigger("Up");
        Ghost2Animator.ResetTrigger("Left");
        Ghost2Animator.ResetTrigger("Right");
        Ghost2Animator.ResetTrigger("Recover");

        Ghost3Animator.ResetTrigger("Down");
        Ghost3Animator.ResetTrigger("Up");
        Ghost3Animator.ResetTrigger("Left");
        Ghost3Animator.ResetTrigger("Right");
        Ghost3Animator.ResetTrigger("Recover");

        Ghost4Animator.ResetTrigger("Down");
        Ghost4Animator.ResetTrigger("Up");
        Ghost4Animator.ResetTrigger("Left");
        Ghost4Animator.ResetTrigger("Right");
        Ghost4Animator.ResetTrigger("Recover");

        Ghost1Animator.SetTrigger("Scared");
        Ghost2Animator.SetTrigger("Scared");
        Ghost3Animator.SetTrigger("Scared");
        Ghost4Animator.SetTrigger("Scared");
        //if (Ghost1Animator.)
        scaredMusic.Play();
        scaredStateActive = true;
        //ScaredMove(Ghost1);
        //ScaredMove(Ghost2);
        //ScaredMove(Ghost3);
        //ScaredMove(Ghost4);

    }

    public void ScaredMove(GameObject ghost) // Follows Ghost 1 Behaviour 
        // Seems to have some kinks in leaving T junction at beginning 
    {
        // Move in a random valid direction that is further than or equal to the distance from Pacstudent to the current grid position 

        RaycastHit2D right = Physics2D.Raycast(ghost.transform.position, Vector2.right, 1.0f, LayerMask.GetMask("Wall"));
        RaycastHit2D left = Physics2D.Raycast(ghost.transform.position, Vector2.left, 1.0f, LayerMask.GetMask("Wall"));
        RaycastHit2D down = Physics2D.Raycast(ghost.transform.position, Vector2.down, 1.0f, LayerMask.GetMask("Wall"));
        RaycastHit2D up = Physics2D.Raycast(ghost.transform.position, Vector2.up, 1.0f, LayerMask.GetMask("Wall"));

        // Move Away PacStudent
        RaycastHit2D psRight = Physics2D.Raycast(ghost.transform.position, Vector2.right, 5.0f, LayerMask.GetMask("PacStudent"));
        RaycastHit2D psLeft = Physics2D.Raycast(ghost.transform.position, Vector2.left, 5.0f, LayerMask.GetMask("PacStudent"));
        RaycastHit2D psDown = Physics2D.Raycast(ghost.transform.position, Vector2.down, 5.0f, LayerMask.GetMask("PacStudent"));
        RaycastHit2D psUp = Physics2D.Raycast(ghost.transform.position, Vector2.up, 5.0f, LayerMask.GetMask("PacStudent"));

        // Set Positions 
        if (ghost == Ghost1 && !setScared1)
        {
            scaredDirVector = dirG1Vector;
            setScared1 = true;
            //Debug.Log("Ghost1 Success");
        }
        else if (ghost == Ghost2 && !setScared2)
        {
            scaredDirVector = dirG2Vector;
            setScared2 = true;
        }
        else if (ghost == Ghost3 && !setScared3)
        {
            scaredDirVector = dirG3Vector;
            setScared3 = true;
        }
        else if (ghost == Ghost4 && !setScared4)
        {
            scaredDirVector = dirG4Vector;
            setScared4 = true;
        }


        if (psUp.collider != null)
        {
            //scaredDirVector = Vector3.up;
            if (right.collider == null && scaredDirVector != Vector3.left)
            {
                scaredDirVector = Vector3.right;
            }
            else if (left.collider == null && scaredDirVector != Vector3.right)
            {
                scaredDirVector = Vector3.left;
            }
            else if (down.collider == null && scaredDirVector != Vector3.up)
            {
                scaredDirVector = Vector3.down;
            }
            else
            {
                scaredDirVector = Vector3.up;
            }

        }
        else if (psDown.collider != null)
        {
            //dirG2Vector = Vector3.down;
            if (right.collider == null && scaredDirVector != Vector3.left)
            {
                scaredDirVector = Vector3.right;
            }
            else if (left.collider == null && scaredDirVector != Vector3.right)
            {
                scaredDirVector = Vector3.left;
            }
            else if (up.collider == null && scaredDirVector != Vector3.down)
            {
                scaredDirVector = Vector3.up;
            }
            else
            {
                scaredDirVector = Vector3.down;
            }
        }
        else if (psRight.collider != null)
        {
            //dirG2Vector = Vector3.right;
            if (up.collider == null && scaredDirVector != Vector3.down)
            {
                scaredDirVector = Vector3.up;
            }
            else if (down.collider == null && scaredDirVector != Vector3.up)
            {
                scaredDirVector = Vector3.down;
            }
            else if (left.collider == null && scaredDirVector != Vector3.right)
            {
                scaredDirVector = Vector3.left;
            }
            else
            {
                scaredDirVector = Vector3.right;
            }
        }
        else if (psLeft.collider != null)
        {
            //dirG2Vector = Vector3.left;
            if (up.collider == null && scaredDirVector != Vector3.down)
            {
                scaredDirVector = Vector3.up;
            }
            else if (down.collider == null && scaredDirVector != Vector3.up)
            {
                scaredDirVector = Vector3.down;
            }
            else if (right.collider == null && scaredDirVector != Vector3.left)
            {
                scaredDirVector = Vector3.right;
            }
            else
            {
                scaredDirVector = Vector3.left;
            }
        }
        /*
        if (down.collider != null && up.collider != null)
        {
            Debug.Log("Up/Down Collide");
            if (scaredDirVector == Vector3.left)
            {
                scaredDirVector = Vector3.left;
                Debug.Log("Issue 1 Here");
            }
            else if (scaredDirVector == Vector3.right)
            {
                scaredDirVector = Vector3.right;
                Debug.Log("Issue 2 Here");
            }
            
        }
        else if (up.collider != null && down.collider == null && ghost.transform.position.y > 7.5) 
        {
            ScaredMoveDirection(2, 4);

        }
        */
        
        if (up.collider == null && scaredDirVector == Vector3.up)
        {
            CreateGhostTween(ghost, ghost.transform.position + scaredDirVector, 1.75f);
            /*if (left.collider == null && right.collider == null && ghost.transform.position.y > 7.5) 
            {
                ScaredMoveDirection(2, 4);
                //Debug.Log("Problem is Here!");
                //scaredDirVector = Vector3.up;
                //CreateGhostTween(ghost, ghost.transform.position + scaredDirVector, 1.75f);

            }*/
            if (left.collider != null && right.collider != null)
            {
                scaredDirVector = Vector3.up;
            }
        }
        else if (down.collider == null && scaredDirVector == Vector3.down)
        {
            //CreateGhostTween(ghost.transform.position + dirVector, 1.75f);
            CreateGhostTween(ghost, ghost.transform.position + scaredDirVector, 1.75f);
            //Debug.Log("Correct");
            if (left.collider != null && right.collider != null)
            {
                scaredDirVector = Vector3.down;
            }

        }
        else if (right.collider == null && scaredDirVector == Vector3.right)
        {
            //Debug.Log("Happy");
            CreateGhostTween(ghost, ghost.transform.position + scaredDirVector, 1.75f);
            if (up.collider != null && down.collider != null)
            {
                scaredDirVector = Vector3.right;
                Debug.Log("Tight Space");
            }
            else if (up.collider == null && left.collider == null && down.collider == null && ghost.transform.position.x > -8.5 && ghost.transform.position.x < 8.5) // NOte this 
            {
                ScaredMoveDirection(0, 3);
                //Debug.Log("Junction1");
                //if (scaredDirVector == Vector3.up || scaredDirVector == Vector3.left || scaredDirVector == Vector3.down)
                //{
                    
                    //CreateGhostTween(ghost, ghost.transform.position + scaredDirVector, 1.75f);
                    //Debug.Log("Problem Here!");
                //}
                //else
                //{
                    //ScaredMoveDirection(0, 3);
                //}
                //Debug.Log("Problem Here!" + ghost.name);
            }
            else if (up.collider == null && ghost.transform.position.y > 2.5 && ghost.transform.position.y < 7.5 && ghost.transform.position.x < 5.5 && ghost.transform.position.x > -0.5) // need this for the beginning
            {
                scaredDirVector = Vector3.up;
            }
            //else if (down.collider == null && left.collider == null && ghost.transform.position.y > 12.5 && ghost.transform.position.x < -12.5)
            //{
            //scaredDirVector = Vector3.down;
            ////Debug.Log("Junction2");
            //CreateGhostTween(ghost, ghost.transform.position + scaredDirVector, 1.75f);

            //}
            
            else
            {
                scaredDirVector = Vector3.right;
                //Debug.Log("Else Right");
            }
        }
        else if (left.collider == null && scaredDirVector == Vector3.left)
        {
            CreateGhostTween(ghost, ghost.transform.position + scaredDirVector, 1.75f);
            if (up.collider != null && down.collider != null)
            {
                scaredDirVector = Vector3.left;
            }
            else if (up.collider == null && right.collider == null && down.collider == null)
            {
                ScaredMoveDirection(0, 4);
                //Debug.Log("Junction5");
                if (scaredDirVector == Vector3.up || scaredDirVector == Vector3.right || scaredDirVector == Vector3.down)
                {
                    CreateGhostTween(ghost, ghost.transform.position + scaredDirVector, 1.75f);
                }
            }

            else if (up.collider == null && ghost.transform.position.y > 2.5 && ghost.transform.position.y < 7.5 && ghost.transform.position.x > -5.5 && ghost.transform.position.x < 0.5) // need this for the beginning
            {
                scaredDirVector = Vector3.up;
            }
            
            /*
            else if (down.collider == null && right.collider == null && ghost.transform.position.y > 12.5)
            {
                scaredDirVector = Vector3.down;
                //Debug.Log("Junction4");
                //CreateGhostTween(ghost, ghost.transform.position + scaredDirVector, 1.75f);
            }
            */
            else
            {
                scaredDirVector = Vector3.left;
            }
        }

        else
        {
            /*
            if (down.collider != null && up.collider != null)
            {
                Debug.Log("Up/Down Collide");
                if (scaredDirVector == Vector3.left)
                {
                    scaredDirVector = Vector3.left;
                    Debug.Log("Issue 1 Here");
                }
                else if (scaredDirVector == Vector3.right)
                {
                    scaredDirVector = Vector3.right;
                    Debug.Log("Issue 2 Here");
                }
                else
                {
                    /* if (ghost == Ghost1)
                    {
                        scaredDirVector = dirG1Vector;
                        //Debug.Log("Ghost1 Success");
                    }
                    else if (ghost == Ghost2)
                    {
                        scaredDirVector = dirG1Vector;
                    }
                    else if (ghost == Ghost3)
                    {
                        scaredDirVector = dirG1Vector;
                    }
                    else if (ghost == Ghost4)
                    {
                        scaredDirVector = dirG1Vector;
                    } */
                    //ScaredMoveDirection(2, 4);
                //}
        //}
    
            if (up.collider != null && ghost.transform.position.y > 7.5) // meant to be else 
            {
                //ScaredMoveDirection(2, 4);
                //CreateGhostTween(ghost, ghost.transform.position + scaredDirVector, 1.75f);

                if (right.collider != null)
                {
                    if (scaredDirVector == Vector3.up)
                    {
                        scaredDirVector = Vector3.left;
                        //Debug.Log("1");

                    }
                    else if (scaredDirVector == Vector3.right)
                    {
                        scaredDirVector = Vector3.down;
                        //Debug.Log("2");
                    }
                    else
                    {
                        scaredDirVector = Vector3.left;
                        //Debug.Log("3");
                    }
                }
                else if (left.collider != null)
                {
                    if (scaredDirVector == Vector3.up)
                    {
                        scaredDirVector = Vector3.right;
                    }
                    else if (scaredDirVector == Vector3.left)
                    {
                        scaredDirVector = Vector3.down;
                    }
                    else
                    {
                        scaredDirVector = Vector3.down;
                    }
                }
                else if (down.collider != null)
                {
                    //Debug.Log("Up Collider Hit");
                    if (scaredDirVector == Vector3.left)
                    {
                        scaredDirVector = Vector3.left;
                    }
                    else if (scaredDirVector == Vector3.right)
                    {
                        scaredDirVector = Vector3.right;
                    }
                    else
                    {
                        if (left.collider == null)
                        {
                            scaredDirVector = Vector3.left;
                        }
                        else if (right.collider == null)
                        {
                            scaredDirVector = Vector3.right;
                        }
                        Debug.Log("Gotcha");
                    }
                }

            }
            else if (down.collider != null && ghost.transform.position.y < -7.5)
            {
                ScaredMoveDirection(2, 4);
                //CreateGhostTween(ghost, ghost.transform.position + scaredDirVector, 1.75f);

                if (right.collider != null)
                {
                    if (scaredDirVector == Vector3.down)
                    {
                        scaredDirVector = Vector3.left;
                    }
                    else if (scaredDirVector == Vector3.left)
                    {
                        scaredDirVector = Vector3.up;
                    }
                }
                else if (left.collider != null)
                {
                    if (scaredDirVector == Vector3.down)
                    {
                        scaredDirVector = Vector3.right;
                    }
                    else if (scaredDirVector == Vector3.right)
                    {
                        scaredDirVector = Vector3.up;
                    }
                }
                else
                {
                    if (scaredDirVector == Vector3.left)
                    {
                        scaredDirVector = Vector3.left;
                    }
                    else if (scaredDirVector == Vector3.right)
                    {
                        scaredDirVector = Vector3.right;
                    }
                    else if (scaredDirVector == Vector3.down)
                    {
                        ScaredMoveDirection(2, 4);
                    }
                }
            }
            else if (down.collider != null && ghost.transform.position.y < 8.5)
            {
                if (left.collider != null && ghost.transform.position.x < -9.5)
                {
                    scaredDirVector = Vector3.right;
                    //Debug.Log("Down/Left Corner");
                }
                else if (right.collider != null && ghost.transform.position.x > 9.5)
                {
                    scaredDirVector = Vector3.left;
                    //Debug.Log("Down/Right Corner");
                }
                else
                {
                    scaredDirVector = Vector3.up;
                }
            }
            else if (up.collider == null && down.collider != null && scaredDirVector == Vector3.down)
            {
                ScaredMoveDirection(2, 4);
            }
            else if (up.collider == null && down.collider == null)
            {
                scaredDirVector = Vector3.up;
            }
            else if (up.collider != null && scaredDirVector != Vector3.left && scaredDirVector != Vector3.right)
            {
                ScaredMoveDirection(2, 4);
            }

        }
    }

    public void ScaredMoveDirection(int min, int max)
    {
        int direction = Random.Range(min, max);
        switch (direction)
        {
            case 0:
                // Up
                scaredDirVector = Vector3.up;
                break;
            case 1:
                // Down 
                scaredDirVector = Vector3.down;
                break;
            case 2:
                // Left
                scaredDirVector = Vector3.left;
                break;
            case 3:
                // Right
                scaredDirVector = Vector3.right;
                break;
            default:
                break;
        }
    }

    public void RecoverState()
    {
        //scaredMusic.Stop();
        //scaredStateActive = false;
        Ghost1Animator.ResetTrigger("Scared");
        Ghost2Animator.ResetTrigger("Scared");
        Ghost3Animator.ResetTrigger("Scared");
        Ghost4Animator.ResetTrigger("Scared");
        Ghost1Animator.SetTrigger("Recover");
        Ghost2Animator.SetTrigger("Recover");
        Ghost3Animator.SetTrigger("Recover");
        Ghost4Animator.SetTrigger("Recover");
        scaredStateActive = false;
    }

    public void DeathState(GameObject collided)
    {
        deathMusic.Play();
        scaredStateActive = false;
        deathStateActive = true;
        /*
        Ghost1Animator.SetTrigger("Death");
        Ghost2Animator.SetTrigger("Death");
        Ghost3Animator.SetTrigger("Death");
        Ghost4Animator.SetTrigger("Death");
        */
        //collided.GetComponent<Animator>.Set
        if (collided == Ghost1)
        {
            Ghost1Animator.SetTrigger("Death");
            DeathStateMove(Ghost1);
        }
        else if (collided == Ghost2)
        {
            Ghost2Animator.SetTrigger("Death");
            DeathStateMove(Ghost2);
        }
        else if (collided == Ghost3)
        {
            Ghost3Animator.SetTrigger("Death");
        }
        else if (collided == Ghost4)
        {
            Ghost4Animator.SetTrigger("Death");
        }
    }

    private void DeathStateMove(GameObject ghost)
    {
        RaycastHit2D right = Physics2D.Raycast(ghost.transform.position, Vector2.right, 10.0f, LayerMask.GetMask("SpawnGate"));
        RaycastHit2D left = Physics2D.Raycast(ghost.transform.position, Vector2.left, 10.0f, LayerMask.GetMask("SpawnGate"));
        RaycastHit2D down = Physics2D.Raycast(ghost.transform.position, Vector2.down, 10.0f, LayerMask.GetMask("SpawnGate"));
        RaycastHit2D up = Physics2D.Raycast(ghost.transform.position, Vector2.up, 10.0f, LayerMask.GetMask("SpawnGate"));

        if (right.collider != null)
        {
            CreateGhostTween(ghost, spawnPoints[0] + Vector3.down, 1.75f);
        }
        else if (left.collider != null)
        {
            CreateGhostTween(ghost, spawnPoints[1] + Vector3.down, 1.75f);
        }
        else if (down.collider != null)
        {
            CreateGhostTween(ghost, spawnPoints[0] + Vector3.down * 2, 1.75f);
        }
        else if (up.collider != null)
        {
            CreateGhostTween(ghost, spawnPoints[1] + Vector3.down * 2, 1.75f);
        }
    }

    public void Ghost1Animation()
    {
        if (dirG1Vector == Vector3.up)
        {
            Ghost1Animator.SetTrigger("Up");
            Ghost1Animator.ResetTrigger("Left");
            Ghost1Animator.ResetTrigger("Right");
            Ghost1Animator.ResetTrigger("Down");
        }
        else if (dirG1Vector == Vector3.down)
        {
            Ghost1Animator.SetTrigger("Down");
            Ghost1Animator.ResetTrigger("Left");
            Ghost1Animator.ResetTrigger("Right");
            Ghost1Animator.ResetTrigger("Up");
        }
        else if (dirG1Vector == Vector3.left)
        {
            Ghost1Animator.SetTrigger("Left");
            Ghost1Animator.ResetTrigger("Down");
            Ghost1Animator.ResetTrigger("Right");
            Ghost1Animator.ResetTrigger("Up");
        }
        else if (dirG1Vector == Vector3.right)
        {
            Ghost1Animator.SetTrigger("Right");
            Ghost1Animator.ResetTrigger("Left");
            Ghost1Animator.ResetTrigger("Down");
            Ghost1Animator.ResetTrigger("Up");
        }
    }

    public void Ghost2Animation()
    {
        if (dirG2Vector == Vector3.up)
        {
            Ghost2Animator.SetTrigger("Up");
            Ghost2Animator.ResetTrigger("Left");
            Ghost2Animator.ResetTrigger("Right");
            Ghost2Animator.ResetTrigger("Down");
        }
        else if (dirG2Vector == Vector3.down)
        {
            Ghost2Animator.SetTrigger("Down");
            Ghost2Animator.ResetTrigger("Left");
            Ghost2Animator.ResetTrigger("Right");
            Ghost2Animator.ResetTrigger("Up");
        }
        else if (dirG2Vector == Vector3.left)
        {
            Ghost2Animator.SetTrigger("Left");
            Ghost2Animator.ResetTrigger("Down");
            Ghost2Animator.ResetTrigger("Right");
            Ghost2Animator.ResetTrigger("Up");
        }
        else if (dirG2Vector == Vector3.right)
        {
            Ghost2Animator.SetTrigger("Right");
            Ghost2Animator.ResetTrigger("Left");
            Ghost2Animator.ResetTrigger("Down");
            Ghost2Animator.ResetTrigger("Up");
        }
    }

    public void Ghost3Animation()
    {
        if (dirG3Vector == Vector3.up)
        {
            Ghost3Animator.SetTrigger("Up");
            Ghost3Animator.ResetTrigger("Left");
            Ghost3Animator.ResetTrigger("Right");
            Ghost3Animator.ResetTrigger("Down");
        }
        else if (dirG3Vector == Vector3.down)
        {
            Ghost3Animator.SetTrigger("Down");
            Ghost3Animator.ResetTrigger("Left");
            Ghost3Animator.ResetTrigger("Right");
            Ghost3Animator.ResetTrigger("Up");
        }
        else if (dirG3Vector == Vector3.left)
        {
            Ghost3Animator.SetTrigger("Left");
            Ghost3Animator.ResetTrigger("Down");
            Ghost3Animator.ResetTrigger("Right");
            Ghost3Animator.ResetTrigger("Up");
        }
        else if (dirG3Vector == Vector3.right)
        {
            Ghost3Animator.SetTrigger("Right");
            Ghost3Animator.ResetTrigger("Left");
            Ghost3Animator.ResetTrigger("Down");
            Ghost3Animator.ResetTrigger("Up");
        }
    }

    public void Ghost4Animation()
    {
        if (dirG4Vector == Vector3.up)
        {
            Ghost4Animator.SetTrigger("Up");
            Ghost4Animator.ResetTrigger("Left");
            Ghost4Animator.ResetTrigger("Right");
            Ghost4Animator.ResetTrigger("Down");
        }
        else if (dirG4Vector == Vector3.down)
        {
            Ghost4Animator.SetTrigger("Down");
            Ghost4Animator.ResetTrigger("Left");
            Ghost4Animator.ResetTrigger("Right");
            Ghost4Animator.ResetTrigger("Up");
        }
        else if (dirG4Vector == Vector3.left)
        {
            Ghost4Animator.SetTrigger("Left");
            Ghost4Animator.ResetTrigger("Down");
            Ghost4Animator.ResetTrigger("Right");
            Ghost4Animator.ResetTrigger("Up");
        }
        else if (dirG4Vector == Vector3.right)
        {
            Ghost4Animator.SetTrigger("Right");
            Ghost4Animator.ResetTrigger("Left");
            Ghost4Animator.ResetTrigger("Down");
            Ghost4Animator.ResetTrigger("Up");
        }
    }

    public void Ghost1Move()
    {
        // Move in a random valid direction that is further than or equal to the distance from Pacstudent to the current grid position 

        RaycastHit2D right = Physics2D.Raycast(Ghost1.transform.position, Vector2.right, 1.0f, LayerMask.GetMask("Wall"));
        RaycastHit2D left = Physics2D.Raycast(Ghost1.transform.position, Vector2.left, 1.0f, LayerMask.GetMask("Wall"));
        RaycastHit2D down = Physics2D.Raycast(Ghost1.transform.position, Vector2.down, 1.0f, LayerMask.GetMask("Wall"));
        RaycastHit2D up = Physics2D.Raycast(Ghost1.transform.position, Vector2.up, 1.0f, LayerMask.GetMask("Wall"));

        // Move Away PacStudent
        RaycastHit2D psRight = Physics2D.Raycast(Ghost1.transform.position, Vector2.right, 5.0f, LayerMask.GetMask("PacStudent"));
        RaycastHit2D psLeft = Physics2D.Raycast(Ghost1.transform.position, Vector2.left, 5.0f, LayerMask.GetMask("PacStudent"));
        RaycastHit2D psDown = Physics2D.Raycast(Ghost1.transform.position, Vector2.down, 5.0f, LayerMask.GetMask("PacStudent"));
        RaycastHit2D psUp = Physics2D.Raycast(Ghost1.transform.position, Vector2.up, 5.0f, LayerMask.GetMask("PacStudent"));

        if (psUp.collider != null)
        {
            //dirG1Vector = Vector3.up;
            if (right.collider == null && dirG1Vector != Vector3.left)
            {
                dirG1Vector = Vector3.right;
            }
            else if (left.collider == null && dirG1Vector != Vector3.right)
            {
                dirG1Vector = Vector3.left;
            }
            else if (down.collider == null && dirG1Vector != Vector3.up)
            {
                dirG1Vector = Vector3.down;
            }
            else
            {
                dirG1Vector = Vector3.up;
            }

        }
        else if (psDown.collider != null)
        {
            //dirG2Vector = Vector3.down;
            if (right.collider == null && dirG1Vector != Vector3.left)
            {
                dirG1Vector = Vector3.right;
            }
            else if (left.collider == null && dirG1Vector != Vector3.right)
            {
                dirG1Vector = Vector3.left;
            }
            else if (up.collider == null && dirG1Vector != Vector3.down)
            {
                dirG1Vector = Vector3.up;
            }
            else
            {
                dirG1Vector = Vector3.down;
            }
        }
        else if (psRight.collider != null)
        {
            //dirG2Vector = Vector3.right;
            if (up.collider == null && dirG1Vector != Vector3.down)
            {
                dirG1Vector = Vector3.up;
            }
            else if (down.collider == null && dirG1Vector != Vector3.up)
            {
                dirG1Vector = Vector3.down;
            }
            else if (left.collider == null && dirG1Vector != Vector3.right)
            {
                dirG1Vector = Vector3.left;
            }
            else
            {
                dirG1Vector = Vector3.right;
            }
        }
        else if (psLeft.collider != null)
        {
            //dirG2Vector = Vector3.left;
            if (up.collider == null && dirG1Vector != Vector3.down)
            {
                dirG1Vector = Vector3.up;
            }
            else if (down.collider == null && dirG1Vector != Vector3.up)
            {
                dirG1Vector = Vector3.down;
            }
            else if (right.collider == null && dirG1Vector != Vector3.left)
            {
                dirG1Vector = Vector3.right;
            }
            else
            {
                dirG1Vector = Vector3.left;
            }
        }


        if (up.collider == null && dirG1Vector == Vector3.up)
        {
            CreateGhostTween(Ghost1, Ghost1.transform.position + dirG1Vector, 1.75f);
            /*if (left.collider == null && right.collider == null && Ghost1.transform.position.y > 7.5) 
            {
                MoveG1Direction(2, 4);
                //Debug.Log("Problem is Here!");
                //dirG1Vector = Vector3.up;
                //CreateGhostTween(Ghost1, Ghost1.transform.position + dirG1Vector, 1.75f);

            }*/
        }
        else if (down.collider == null && dirG1Vector == Vector3.down)
        {
            //CreateGhostTween(Ghost1.transform.position + dirVector, 1.75f);
            CreateGhostTween(Ghost1, Ghost1.transform.position + dirG1Vector, 1.75f);
            //Debug.Log("Correct");
        }
        else if (right.collider == null && dirG1Vector == Vector3.right)
        {
            CreateGhostTween(Ghost1, Ghost1.transform.position + dirG1Vector, 1.75f);
            if (up.collider == null && left.collider == null && down.collider == null) // NOte this 
            {
                MoveG1Direction(0, 3);
                //Debug.Log("Junction1");
                if (dirG1Vector == Vector3.up || dirG1Vector == Vector3.left || dirG1Vector == Vector3.down)
                {
                    CreateGhostTween(Ghost1, Ghost1.transform.position + dirG1Vector, 1.75f);
                }

            }
            else if (up.collider == null && Ghost1.transform.position.y > 2.5 && Ghost1.transform.position.y < 7.5 && Ghost1.transform.position.x < 5.5 && Ghost1.transform.position.x > -0.5) // need this for the beginning
            {
                dirG1Vector = Vector3.up;
            }
            //else if (down.collider == null && left.collider == null && Ghost1.transform.position.y > 12.5 && Ghost1.transform.position.x < -12.5)
            //{
            //dirG1Vector = Vector3.down;
            ////Debug.Log("Junction2");
            //CreateGhostTween(Ghost1, Ghost1.transform.position + dirG1Vector, 1.75f);

            //}
            else
            {
                dirG1Vector = Vector3.right;
                //Debug.Log("Else Right");
            }
        }
        else if (left.collider == null && dirG1Vector == Vector3.left)
        {
            CreateGhostTween(Ghost1, Ghost1.transform.position + dirG1Vector, 1.75f);

            if (up.collider == null && right.collider == null && down.collider == null)
            {
                MoveG1Direction(0, 4);
                //Debug.Log("Junction5");
                if (dirG1Vector == Vector3.up || dirG1Vector == Vector3.right || dirG1Vector == Vector3.down)
                {
                    CreateGhostTween(Ghost1, Ghost1.transform.position + dirG1Vector, 1.75f);
                }
            }

            else if (up.collider == null && Ghost1.transform.position.y > 2.5 && Ghost1.transform.position.y < 7.5 && Ghost1.transform.position.x > -5.5 && Ghost1.transform.position.x < 0.5) // need this for the beginning
            {
                dirG1Vector = Vector3.up;
            }
            /*
            else if (down.collider == null && right.collider == null && Ghost1.transform.position.y > 12.5)
            {
                dirG1Vector = Vector3.down;
                //Debug.Log("Junction4");
                //CreateGhostTween(Ghost1, Ghost1.transform.position + dirG1Vector, 1.75f);
            }
            */
            else
            {
                dirG1Vector = Vector3.left;
            }
        }

        else
        {
            if (down.collider != null && up.collider != null)
            {
                if (dirG1Vector == Vector3.left)
                {
                    dirG1Vector = Vector3.left;
                }
                else if (dirG1Vector == Vector3.right)
                {
                    dirG1Vector = Vector3.right;
                }
            }
            else if (up.collider != null && Ghost1.transform.position.y > 7.5)
            {
                //MoveG1Direction(2, 4);
                //CreateGhostTween(Ghost1, Ghost1.transform.position + dirG1Vector, 1.75f);

                if (right.collider != null)
                {
                    if (dirG1Vector == Vector3.up)
                    {
                        dirG1Vector = Vector3.left;
                        //Debug.Log("1");

                    }
                    else if (dirG1Vector == Vector3.right)
                    {
                        dirG1Vector = Vector3.down;
                        //Debug.Log("2");
                    }
                    else
                    {
                        dirG1Vector = Vector3.left;
                        //Debug.Log("3");
                    }
                }
                else if (left.collider != null)
                {
                    if (dirG1Vector == Vector3.up)
                    {
                        dirG1Vector = Vector3.right;
                    }
                    else if (dirG1Vector == Vector3.left)
                    {
                        dirG1Vector = Vector3.down;
                    }
                    else
                    {
                        dirG1Vector = Vector3.down;
                    }
                }
                else
                {
                    //Debug.Log("Up Collider Hit");
                    if (dirG1Vector == Vector3.left)
                    {
                        dirG1Vector = Vector3.left;
                    }
                    else if (dirG1Vector == Vector3.right)
                    {
                        dirG1Vector = Vector3.right;
                    }
                    else
                    {
                        MoveG1Direction(2, 4);
                    }
                }

            }
            else if (down.collider != null && Ghost1.transform.position.y < -7.5)
            {
                MoveG1Direction(2, 4);
                //CreateGhostTween(Ghost1, Ghost1.transform.position + dirG1Vector, 1.75f);

                if (right.collider != null)
                {
                    if (dirG1Vector == Vector3.down)
                    {
                        dirG1Vector = Vector3.left;
                    }
                    else if (dirG1Vector == Vector3.left)
                    {
                        dirG1Vector = Vector3.up;
                    }
                }
                else if (left.collider != null)
                {
                    if (dirG1Vector == Vector3.down)
                    {
                        dirG1Vector = Vector3.right;
                    }
                    else if (dirG1Vector == Vector3.right)
                    {
                        dirG1Vector = Vector3.up;
                    }
                }
                else
                {
                    if (dirG1Vector == Vector3.left)
                    {
                        dirG1Vector = Vector3.left;
                    }
                    else if (dirG1Vector == Vector3.right)
                    {
                        dirG1Vector = Vector3.right;
                    }
                    else if (dirG1Vector == Vector3.down)
                    {
                        MoveG1Direction(2, 4);
                    }
                }
            }
            else if (down.collider != null && Ghost1.transform.position.y < 8.5)
            {
                if (left.collider != null && Ghost1.transform.position.x < -9.5)
                {
                    dirG1Vector = Vector3.right;
                    //Debug.Log("Down/Left Corner");
                }
                else if (right.collider != null && Ghost1.transform.position.x > 9.5)
                {
                    dirG1Vector = Vector3.left;
                    //Debug.Log("Down/Right Corner");
                }
                else
                {
                    dirG1Vector = Vector3.up;
                }
            }
            else if (up.collider == null && down.collider != null && dirG1Vector == Vector3.down)
            {
                MoveG1Direction(2, 4);
            }
            else if (up.collider == null && down.collider == null)
            {
                dirG1Vector = Vector3.up;
            }
            else if (up.collider != null)
            {
                MoveG1Direction(2, 4);
            }
            
        }

    }


    private void MoveG1Direction(int min, int max)
    {
        int direction = Random.Range(min, max);
        switch (direction)
        {
            case 0:
                // Up
                dirG1Vector = Vector3.up;
                break;
            case 1:
                // Down 
                dirG1Vector = Vector3.down;
                break;
            case 2:
                // Left
                dirG1Vector = Vector3.left;
                break;
            case 3:
                // Right
                dirG1Vector = Vector3.right;
                break;
            default:
                break;
        }
    }

    public void Ghost2Move()
    {
        // Move to a random adjacent junction that is closer than or equal to PacStudent than the current grid position 

        RaycastHit2D right = Physics2D.Raycast(Ghost2.transform.position, Vector2.right, 1.0f, LayerMask.GetMask("Wall"));
        RaycastHit2D left = Physics2D.Raycast(Ghost2.transform.position, Vector2.left, 1.0f, LayerMask.GetMask("Wall"));
        RaycastHit2D down = Physics2D.Raycast(Ghost2.transform.position, Vector2.down, 1.0f, LayerMask.GetMask("Wall"));
        RaycastHit2D up = Physics2D.Raycast(Ghost2.transform.position, Vector2.up, 1.0f, LayerMask.GetMask("Wall"));

        // Use Random Movement of Ghost 3 

        // Move Towards PacStudent
        RaycastHit2D psRight = Physics2D.Raycast(Ghost2.transform.position, Vector2.right, 5.0f, LayerMask.GetMask("PacStudent"));
        RaycastHit2D psLeft = Physics2D.Raycast(Ghost2.transform.position, Vector2.left, 5.0f, LayerMask.GetMask("PacStudent"));
        RaycastHit2D psDown = Physics2D.Raycast(Ghost2.transform.position, Vector2.down, 5.0f, LayerMask.GetMask("PacStudent"));
        RaycastHit2D psUp = Physics2D.Raycast(Ghost2.transform.position, Vector2.up, 5.0f, LayerMask.GetMask("PacStudent"));


        // Move Towards PacStudent
        if (psUp.collider != null)
        {
            dirG2Vector = Vector3.up;
        }
        else if (psDown.collider != null)
        {
            dirG2Vector = Vector3.down;
        }
        else if (psRight.collider != null)
        {
            dirG2Vector = Vector3.right;
        }
        else if (psLeft.collider != null)
        {
            dirG2Vector = Vector3.left;
        }

        if (up.collider == null && dirG2Vector == Vector3.up)
        {
            CreateGhostTween(Ghost2, Ghost2.transform.position + dirG2Vector, 1.75f);
            /*if (left.collider == null && right.collider == null && Ghost2.transform.position.y > 7.5) 
            {
                MoveG2Direction(2, 4);
                //Debug.Log("Problem is Here!");
                //dirG2Vector = Vector3.up;
                //CreateGhostTween(Ghost2, Ghost2.transform.position + dirG2Vector, 1.75f);

            }*/
        }
        else if (down.collider == null && dirG2Vector == Vector3.down)
        {
            //CreateGhostTween(Ghost2.transform.position + dirVector, 1.75f);
            CreateGhostTween(Ghost2, Ghost2.transform.position + dirG2Vector, 1.75f);
            //Debug.Log("Correct");
        }
        else if (right.collider == null && dirG2Vector == Vector3.right)
        {
            CreateGhostTween(Ghost2, Ghost2.transform.position + dirG2Vector, 1.75f);
            if (up.collider == null && left.collider == null && down.collider == null) // NOte this 
            {
                MoveG2Direction(0, 3);
                //Debug.Log("Junction1");
                if (dirG2Vector == Vector3.up || dirG2Vector == Vector3.left || dirG2Vector == Vector3.down)
                {
                    CreateGhostTween(Ghost2, Ghost2.transform.position + dirG2Vector, 1.75f);
                }

            }
            else if (up.collider == null && Ghost2.transform.position.y > 2.5 && Ghost2.transform.position.y < 7.5 && Ghost2.transform.position.x < 5.5 && Ghost2.transform.position.x > -0.5) // need this for the beginning
            {
                dirG2Vector = Vector3.up;
            }
            //else if (down.collider == null && left.collider == null && Ghost2.transform.position.y > 12.5 && Ghost2.transform.position.x < -12.5)
            //{
            //dirG2Vector = Vector3.down;
            ////Debug.Log("Junction2");
            //CreateGhostTween(Ghost2, Ghost2.transform.position + dirG2Vector, 1.75f);

            //}
            else
            {
                dirG2Vector = Vector3.right;
                //Debug.Log("Else Right");
            }
        }
        else if (left.collider == null && dirG2Vector == Vector3.left)
        {
            CreateGhostTween(Ghost2, Ghost2.transform.position + dirG2Vector, 1.75f);

            if (up.collider == null && right.collider == null && down.collider == null)
            {
                MoveG2Direction(0, 4);
                //Debug.Log("Junction5");
                if (dirG2Vector == Vector3.up || dirG2Vector == Vector3.right || dirG2Vector == Vector3.down)
                {
                    CreateGhostTween(Ghost2, Ghost2.transform.position + dirG2Vector, 1.75f);
                }
            }

            else if (up.collider == null && Ghost2.transform.position.y > 2.5 && Ghost2.transform.position.y < 7.5 && Ghost2.transform.position.x > -5.5 && Ghost2.transform.position.x < 0.5) // need this for the beginning
            {
                dirG2Vector = Vector3.up;
            }
            /*
            else if (down.collider == null && right.collider == null && Ghost2.transform.position.y > 12.5)
            {
                dirG2Vector = Vector3.down;
                //Debug.Log("Junction4");
                //CreateGhostTween(Ghost2, Ghost2.transform.position + dirG2Vector, 1.75f);
            }
            */
            else
            {
                dirG2Vector = Vector3.left;
            }
        }

        else
        {
            if (down.collider != null && up.collider != null)
            {
                if (dirG2Vector == Vector3.left)
                {
                    dirG2Vector = Vector3.left;
                }
                else if (dirG2Vector == Vector3.right)
                {
                    dirG2Vector = Vector3.right;
                }
            }
            else if (up.collider != null && Ghost2.transform.position.y > 7.5)
            {
                //MoveG2Direction(2, 4);
                //CreateGhostTween(Ghost2, Ghost2.transform.position + dirG2Vector, 1.75f);

                if (right.collider != null)
                {
                    if (dirG2Vector == Vector3.up)
                    {
                        dirG2Vector = Vector3.left;
                        //Debug.Log("1");

                    }
                    else if (dirG2Vector == Vector3.right)
                    {
                        dirG2Vector = Vector3.down;
                        //Debug.Log("2");
                    }
                    else
                    {
                        dirG2Vector = Vector3.left;
                        //Debug.Log("3");
                    }
                }
                else if (left.collider != null)
                {
                    if (dirG2Vector == Vector3.up)
                    {
                        dirG2Vector = Vector3.right;
                    }
                    else if (dirG2Vector == Vector3.left)
                    {
                        dirG2Vector = Vector3.down;
                    }
                    else
                    {
                        dirG2Vector = Vector3.down;
                    }
                }
                else
                {
                    //Debug.Log("Up Collider Hit");
                    if (dirG2Vector == Vector3.left)
                    {
                        dirG2Vector = Vector3.left;
                    }
                    else if (dirG2Vector == Vector3.right)
                    {
                        dirG2Vector = Vector3.right;
                    }
                    else
                    {
                        MoveG2Direction(2, 4);
                    }
                }

            }
            else if (down.collider != null && Ghost2.transform.position.y < -7.5)
            {
                MoveG2Direction(2, 4);
                //CreateGhostTween(Ghost2, Ghost2.transform.position + dirG2Vector, 1.75f);

                if (right.collider != null)
                {
                    if (dirG2Vector == Vector3.down)
                    {
                        dirG2Vector = Vector3.left;
                    }
                    else if (dirG2Vector == Vector3.left)
                    {
                        dirG2Vector = Vector3.up;
                    }
                }
                else if (left.collider != null)
                {
                    if (dirG2Vector == Vector3.down)
                    {
                        dirG2Vector = Vector3.right;
                    }
                    else if (dirG2Vector == Vector3.right)
                    {
                        dirG2Vector = Vector3.up;
                    }
                }
                else
                {
                    if (dirG2Vector == Vector3.left)
                    {
                        dirG2Vector = Vector3.left;
                    }
                    else if (dirG2Vector == Vector3.right)
                    {
                        dirG2Vector = Vector3.right;
                    }
                    else if (dirG2Vector == Vector3.down)
                    {
                        MoveG2Direction(2, 4);
                    }
                }
            }
            else if (down.collider != null && Ghost2.transform.position.y < 8.5)
            {
                if (left.collider != null && Ghost2.transform.position.x < -9.5)
                {
                    dirG2Vector = Vector3.right;
                    //Debug.Log("Down/Left Corner");
                }
                else if (right.collider != null && Ghost2.transform.position.x > 9.5)
                {
                    dirG2Vector = Vector3.left;
                    //Debug.Log("Down/Right Corner");
                }
                else
                {
                    dirG2Vector = Vector3.up;
                }
            }
            else if (up.collider == null && down.collider != null && dirG2Vector == Vector3.down)
            {
                MoveG2Direction(2, 4);
            }
            else if (up.collider == null && down.collider == null)
            {
                dirG2Vector = Vector3.up;
            }
            else if (up.collider != null)
            {
                MoveG2Direction(2, 4);
            }
            /*
            else if (left.collider == null && dirG2Vector != Vector3.left)
            {
                //MoveG2Direction(2, 4);
                dirG2Vector = Vector3.left;
                //Debug.Log("Left/Right");
            }
            else if (right.collider == null && dirG2Vector != Vector3.right)
            {
                //MoveG2Direction(2, 4);
                dirG2Vector = Vector3.right;
                ////Debug.Log("Left/Right");
            }
            */
            /*
            else if (right.collider == null)
            {
                dirG2Vector = Vector3.right;
            } 
            else if (down.collider == null)
            {
                dirG2Vector = Vector3.down;
            }
            */
        }


        //if ()
        //CreateGhostTween(Ghost2.transform.position + dirVector, 1.75f);
        /*else
        {
            if (pacStudent.transform.position.sqrMagnitude < 10)
            {
                // Move Towards PacStudent
                RaycastHit2D psRight = Physics2D.Raycast(Ghost2.transform.position, Vector2.right, 5.0f, LayerMask.GetMask("PacStudent"));
                RaycastHit2D psLeft = Physics2D.Raycast(Ghost2.transform.position, Vector2.left, 5.0f, LayerMask.GetMask("PacStudent"));
                RaycastHit2D psDown = Physics2D.Raycast(Ghost2.transform.position, Vector2.down, 5.0f, LayerMask.GetMask("PacStudent"));
                RaycastHit2D psUp = Physics2D.Raycast(Ghost2.transform.position, Vector2.up, 5.0f, LayerMask.GetMask("PacStudent"));


                // Move Towards PacStudent
                if (psUp.collider != null)
                {
                    dirG2Vector = Vector3.up;
                }
                else if (psDown.collider != null)
                {
                    dirG2Vector = Vector3.down;
                }
                else if (psRight.collider != null)
                {
                    dirG2Vector = Vector3.right;
                }
                else if (psLeft.collider != null)
                {
                    dirG2Vector = Vector3.left;
                }
            }
        }*/

    }

    private void MoveG2Direction(int min, int max)
    {
        int direction = Random.Range(min, max);
        switch (direction)
        {
            case 0:
                // Up
                dirG2Vector = Vector3.up;
                break;
            case 1:
                // Down 
                dirG2Vector = Vector3.down;
                break;
            case 2:
                // Left
                dirG2Vector = Vector3.left;
                break;
            case 3:
                // Right
                dirG2Vector = Vector3.right;
                break;
            default:
                break;
        }
    }

    public void Ghost3Move()
    {
        // Move randomly at grid position
        
        RaycastHit2D right = Physics2D.Raycast(Ghost3.transform.position, Vector2.right, 1.0f, LayerMask.GetMask("Wall"));
        RaycastHit2D left = Physics2D.Raycast(Ghost3.transform.position, Vector2.left, 1.0f, LayerMask.GetMask("Wall"));
        RaycastHit2D down = Physics2D.Raycast(Ghost3.transform.position, Vector2.down, 1.0f, LayerMask.GetMask("Wall"));
        RaycastHit2D up = Physics2D.Raycast(Ghost3.transform.position, Vector2.up, 1.0f, LayerMask.GetMask("Wall"));

        if (up.collider == null && dirG3Vector == Vector3.up)
        {
            CreateGhostTween(Ghost3, Ghost3.transform.position + dirG3Vector, 1.75f);
            /*if (left.collider == null && right.collider == null && Ghost3.transform.position.y > 7.5) 
            {
                MoveG3Direction(2, 4);
                //Debug.Log("Problem is Here!");
                //dirG3Vector = Vector3.up;
                //CreateGhostTween(Ghost3, Ghost3.transform.position + dirG3Vector, 1.75f);

            }*/
        }
        else if (down.collider == null && dirG3Vector == Vector3.down)
        {
            //CreateGhostTween(Ghost3.transform.position + dirVector, 1.75f);
            CreateGhostTween(Ghost3, Ghost3.transform.position + dirG3Vector, 1.75f);
            //Debug.Log("Correct");
        }
        else if (right.collider == null && dirG3Vector == Vector3.right)
        {
            CreateGhostTween(Ghost3, Ghost3.transform.position + dirG3Vector, 1.75f);
            if (up.collider == null && left.collider == null && down.collider == null) // NOte this 
            {
                MoveG3Direction(0, 3);
                //Debug.Log("Junction1");
                if (dirG3Vector == Vector3.up || dirG3Vector == Vector3.left || dirG3Vector == Vector3.down)
                {
                    CreateGhostTween(Ghost3, Ghost3.transform.position + dirG3Vector, 1.75f);
                }
                
            }
            else if (up.collider == null && Ghost3.transform.position.y > 2.5 && Ghost3.transform.position.y < 7.5 && Ghost3.transform.position.x < 5.5 && Ghost3.transform.position.x > -0.5) // need this for the beginning
            {
                dirG3Vector = Vector3.up;
            } 
            //else if (down.collider == null && left.collider == null && Ghost3.transform.position.y > 12.5 && Ghost3.transform.position.x < -12.5)
            //{
                //dirG3Vector = Vector3.down;
                ////Debug.Log("Junction2");
                //CreateGhostTween(Ghost3, Ghost3.transform.position + dirG3Vector, 1.75f);

            //}
            else
            {
                dirG3Vector = Vector3.right;
                //Debug.Log("Else Right");
            }
        }
        else if (left.collider == null && dirG3Vector == Vector3.left)
        {
            CreateGhostTween(Ghost3, Ghost3.transform.position + dirG3Vector, 1.75f);

            if (up.collider == null && right.collider == null && down.collider == null)
            {
                MoveG3Direction(0, 4);
                //Debug.Log("Junction5");
                if (dirG3Vector == Vector3.up || dirG3Vector == Vector3.right || dirG3Vector == Vector3.down)
                {
                    CreateGhostTween(Ghost3, Ghost3.transform.position + dirG3Vector, 1.75f);
                }
            }
            
            else if (up.collider == null && Ghost3.transform.position.y > 2.5 && Ghost3.transform.position.y < 7.5 && Ghost3.transform.position.x > -5.5 && Ghost3.transform.position.x < 0.5) // need this for the beginning
            {
                dirG3Vector = Vector3.up;
            } 
            /*
            else if (down.collider == null && right.collider == null && Ghost3.transform.position.y > 12.5)
            {
                dirG3Vector = Vector3.down;
                //Debug.Log("Junction4");
                //CreateGhostTween(Ghost3, Ghost3.transform.position + dirG3Vector, 1.75f);
            }
            */
            else
            {
                dirG3Vector = Vector3.left;
            }
        }
        
        else
        {
            if (down.collider != null && up.collider != null)
            {
                if (dirG3Vector == Vector3.left)
                {
                    dirG3Vector = Vector3.left;
                }
                else if (dirG3Vector == Vector3.right)
                {
                    dirG3Vector = Vector3.right;
                }
            }
            else if (up.collider != null && Ghost3.transform.position.y > 7.5)
            {
                //MoveG3Direction(2, 4);
                //CreateGhostTween(Ghost3, Ghost3.transform.position + dirG3Vector, 1.75f);
                
                if (right.collider != null)
                {
                    if (dirG3Vector == Vector3.up)
                    {
                        dirG3Vector = Vector3.left;
                        //Debug.Log("1");
                        
                    }
                    else if (dirG3Vector == Vector3.right)
                    {
                        dirG3Vector = Vector3.down;
                        //Debug.Log("2");
                    }
                    else
                    {
                        dirG3Vector = Vector3.left;
                        //Debug.Log("3");
                    }
                }
                else if (left.collider != null)
                {
                    if (dirG3Vector == Vector3.up)
                    {
                        dirG3Vector = Vector3.right;
                    }
                    else if (dirG3Vector == Vector3.left)
                    {
                        dirG3Vector = Vector3.down;
                    }
                    else
                    {
                        dirG3Vector = Vector3.down;
                    }
                }
                else
                {
                    //Debug.Log("Up Collider Hit");
                    if (dirG3Vector == Vector3.left)
                    {
                        dirG3Vector = Vector3.left;
                    }
                    else if (dirG3Vector == Vector3.right)
                    {
                        dirG3Vector = Vector3.right;
                    }
                    else
                    {
                        MoveG3Direction(2, 4);
                    }
                }
                
            }
            else if (down.collider != null && Ghost3.transform.position.y < -7.5)
            {
                MoveG3Direction(2, 4);
                //CreateGhostTween(Ghost3, Ghost3.transform.position + dirG3Vector, 1.75f);

                if (right.collider != null)
                {
                    if (dirG3Vector == Vector3.down)
                    {
                        dirG3Vector = Vector3.left;
                    }
                    else if (dirG3Vector == Vector3.left)
                    {
                        dirG3Vector = Vector3.up;
                    }
                }
                else if (left.collider != null)
                {
                    if (dirG3Vector == Vector3.down)
                    {
                        dirG3Vector = Vector3.right;
                    }
                    else if (dirG3Vector == Vector3.right)
                    {
                        dirG3Vector = Vector3.up;
                    }
                }
                else
                {
                    if (dirG3Vector == Vector3.left)
                    {
                        dirG3Vector = Vector3.left;
                    }
                    else if (dirG3Vector == Vector3.right)
                    {
                        dirG3Vector = Vector3.right;
                    }
                    else if (dirG3Vector == Vector3.down)
                    {
                        MoveG3Direction(2, 4);
                    }
                }
            }
            else if (down.collider != null && Ghost3.transform.position.y < 8.5)
            {
                if (left.collider != null && Ghost3.transform.position.x < -9.5)
                {
                    dirG3Vector = Vector3.right;
                    //Debug.Log("Down/Left Corner");
                }
                else if (right.collider != null && Ghost3.transform.position.x > 9.5)
                {
                    dirG3Vector = Vector3.left;
                    //Debug.Log("Down/Right Corner");
                }
                else
                {
                    dirG3Vector = Vector3.up;
                }
            }
            else if (up.collider == null && down.collider != null && dirG3Vector == Vector3.down)
            {
                MoveG3Direction(2, 4);
            }
            else if (up.collider == null && down.collider == null)
            {
                dirG3Vector = Vector3.up;
            }
            else if (up.collider != null)
            {
                MoveG3Direction(2, 4);
            }
            /*
            else if (left.collider == null && dirG3Vector != Vector3.left)
            {
                //MoveG3Direction(2, 4);
                dirG3Vector = Vector3.left;
                //Debug.Log("Left/Right");
            }
            else if (right.collider == null && dirG3Vector != Vector3.right)
            {
                //MoveG3Direction(2, 4);
                dirG3Vector = Vector3.right;
                ////Debug.Log("Left/Right");
            }
            */
            /*
            else if (right.collider == null)
            {
                dirG3Vector = Vector3.right;
            } 
            else if (down.collider == null)
            {
                dirG3Vector = Vector3.down;
            }
            */
        }
        

        //if ()
        //CreateGhostTween(Ghost3.transform.position + dirVector, 1.75f);

    }

    private void MoveG3Direction(int min, int max)
    {
        int direction = Random.Range(min, max);
        switch(direction)
        {
            case 0:
                // Up
                dirG3Vector = Vector3.up;
                break;
            case 1:
                // Down 
                dirG3Vector = Vector3.down;
                break;
            case 2:
                // Left
                dirG3Vector = Vector3.left;
                break;
            case 3:
                // Right
                dirG3Vector = Vector3.right;
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.collider.name);
    }

    public void Ghost4Move()
    {
        // Move clockwise around the map, following the outside wall
        //CreateGhostTween(new Vector3(10.5f, -0.5f, 0), 1f);
        //dirG4Vector = Vector3.up;

        //Ghost4Animation();

        RaycastHit2D right = Physics2D.Raycast(Ghost4.transform.position, Vector2.right, 1.0f, LayerMask.GetMask("Wall"));
        RaycastHit2D left = Physics2D.Raycast(Ghost4.transform.position, Vector2.left, 1.0f, LayerMask.GetMask("Wall"));
        RaycastHit2D down = Physics2D.Raycast(Ghost4.transform.position, Vector2.down, 1.0f, LayerMask.GetMask("Wall"));
        RaycastHit2D up = Physics2D.Raycast(Ghost4.transform.position, Vector2.up, 1.0f, LayerMask.GetMask("Wall"));

        if (up.collider == null && dirG4Vector == Vector3.up)
        {
            CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);

            if (left.collider == null && right.collider != null && Ghost4.transform.position.y > 4.5 && Ghost4.transform.position.x < -0.5)
            {
                dirG4Vector = Vector3.left;
                CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);
                //Ghost4Animation();
            }

        }
        else if (right.collider == null && dirG4Vector == Vector3.right)
        {   
            CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);
            ////Debug.Log("Going Right!");
            //Ghost4Animation();
            if (up.collider == null && left.collider == null && Ghost4.transform.position.x > 0.5)
            {
                dirG4Vector = Vector3.up;
                CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);
                ////Debug.Log("Pizza");
            }

        }
        else if (left.collider == null && dirG4Vector == Vector3.left)
        {
            CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);

            if (down.collider == null && left.collider == null && Ghost4.transform.position.x < 0.5 && Ghost4.transform.position.x > -1.5)
            {
                dirG4Vector = Vector3.down;
                CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);
                ////Debug.Log("Point");
            }


        }
        else if (down.collider == null && dirG4Vector == Vector3.down)
        {
            //CreateGhostTween(Ghost3.transform.position + dirVector, 1.75f);
            CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);
            ////Debug.Log("Going Down");

            if (left.collider != null && right.collider == null && down.collider == null && Ghost4.transform.position.y < -0.5)
            {
                dirG4Vector = Vector3.right;
                CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);
            }
            
        }
        else
        {

            if (left.collider != null && down.collider != null && Ghost4.transform.position.y < -0.5)
            {
                dirG4Vector = Vector3.up;
                CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);
            }
            
            else if (up.collider != null && left.collider == null && Ghost4.transform.position.y < -8.5)
            {
                dirG4Vector = Vector3.left;
                CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);
            }
            
            else if (up.collider != null && right.collider == null)
            {
                dirG4Vector = Vector3.right;
                CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);
            }
            else if (up.collider != null && right.collider != null)
            {
                dirG4Vector = Vector3.down;
                CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);
                ////Debug.Log("Going down!");
            }
            else if (up.collider != null && left.collider != null)
            {
                dirG4Vector = Vector3.right;
                CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);
            }
            else if (down.collider != null && left.collider != null)
            {
                dirG4Vector = Vector3.up;
                CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);
            }
            else if (down.collider != null && right.collider == null)
            {
                dirG4Vector = Vector3.right;
                CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);
            }
            
            else if (down.collider != null && right.collider != null)
            {
                if (dirG4Vector == Vector3.right)
                {
                    dirG4Vector = Vector3.up;
                    CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);
                }
                else if (dirG4Vector == Vector3.down)
                {
                    dirG4Vector = Vector3.left;
                    CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);
                }
            }

            else if (left.collider != null && down.collider == null && dirG4Vector == Vector3.left)
            {
                dirG4Vector = Vector3.down;
                CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);
            }
            else if (right.collider != null && dirG4Vector == Vector3.right)
            {
                dirG4Vector = Vector3.up;
                CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);
            }
            

        }



        //CreateGhostTween(new Vector3(0.5f, 3.5f, 0), 2f);
        //CreateGhostTween(ghost4Pos[1], 2f);
        //StartCoroutine(clockwiseWallMove());

    }

    private IEnumerator clockwiseWallMove()
    {
        //return null;
        while (cycleCount == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                //CreateGhostTween(ghost4Pos[i], 2f);
            }
            yield return new WaitForSeconds(0.5f);
        }

    }

    public void CreateGhostTween(GameObject target, Vector3 endPosition, float duration)
    {
        //if (endPosition.x > -13.5f && endPosition.x < 13.5f && endPosition.y > -14.5f && endPosition.y < 14.5f)
        //{
        bool addedTween = ghost3Tweener.AddTween(target.transform, target.transform.position, endPosition, duration);
        //}
    }

}
