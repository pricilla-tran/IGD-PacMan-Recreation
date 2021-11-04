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
    public AudioSource scaredMusic;
    public GameObject pacStudent;
    private PacStudentController psController;

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

        psController = pacStudent.GetComponent<PacStudentController>();

        Ghost1Animator = Ghost1.GetComponent<Animator>();
        Ghost2Animator = Ghost2.GetComponent<Animator>();
        Ghost3Animator = Ghost3.GetComponent<Animator>();
        Ghost4Animator = Ghost4.GetComponent<Animator>();

        ghost1Tweener = Ghost1.GetComponent<Tweener>();
        ghost2Tweener = Ghost2.GetComponent<Tweener>();
        ghost3Tweener = Ghost3.GetComponent<Tweener>();
        ghost4Tweener = Ghost4.GetComponent<Tweener>();

        Ghost1Animator.ResetTrigger("Down");
        Ghost1Animator.SetTrigger("Up");
        Ghost1Animator.ResetTrigger("Left");
        Ghost1Animator.ResetTrigger("Right");

        //GhostAnimation();

        //MoveG3Direction();

    }

    // Update is called once per frame
    void Update()
    {
        Ghost1Move();
        Ghost2Move();
        Ghost3Move();
        Ghost4Move();
        Ghost4Animation();
    }

    public void ScaredState()
    {
        Ghost1Animator.ResetTrigger("Down");
        Ghost1Animator.ResetTrigger("Up");
        Ghost1Animator.ResetTrigger("Left");
        Ghost1Animator.ResetTrigger("Right");
        Ghost1Animator.SetTrigger("Scared");
        Ghost2Animator.SetTrigger("Scared");
        Ghost3Animator.SetTrigger("Scared");
        Ghost4Animator.SetTrigger("Scared");
        //if (Ghost1Animator.)
        scaredMusic.Play();

    }

    public void RecoverState()
    {
        //scaredMusic.Stop();
        Ghost1Animator.ResetTrigger("Scared");
        Ghost2Animator.ResetTrigger("Scared");
        Ghost3Animator.ResetTrigger("Scared");
        Ghost4Animator.ResetTrigger("Scared");
        Ghost1Animator.SetTrigger("Recover");
        Ghost2Animator.SetTrigger("Recover");
        Ghost3Animator.SetTrigger("Recover");
        Ghost4Animator.SetTrigger("Recover");
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

        if (up.collider == null && dirG1Vector == Vector3.up)
        {
            CreateGhostTween(Ghost1, Ghost1.transform.position + dirG1Vector, 1.75f);
        }
        else if (down.collider == null && dirG1Vector == Vector3.down)
        {
            CreateGhostTween(Ghost1, Ghost1.transform.position + dirG1Vector, 1.75f);
        }
        else if (left.collider == null && dirG1Vector == Vector3.left)
        {
            CreateGhostTween(Ghost1, Ghost1.transform.position + dirG1Vector, 1.75f);
        }
        else if (right.collider == null && dirG1Vector == Vector3.right)
        {
            CreateGhostTween(Ghost1, Ghost1.transform.position + dirG1Vector, 1.75f);
        }
        else
        {
            if (pacStudent.transform.position.sqrMagnitude < 10)
            {
                // Move Away PacStudent if valid
            }
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
       
        if (up.collider == null && dirG2Vector == Vector3.up)
        {
            CreateGhostTween(Ghost2, Ghost2.transform.position + dirG2Vector, 1.75f);
        }
        else if (down.collider == null && dirG2Vector == Vector3.down)
        {
            CreateGhostTween(Ghost2, Ghost2.transform.position + dirG2Vector, 1.75f);
        }
        else if (left.collider == null && dirG2Vector == Vector3.left)
        {
            CreateGhostTween(Ghost2, Ghost2.transform.position + dirG2Vector, 1.75f);
        }
        else if (right.collider == null && dirG2Vector == Vector3.right)
        {
            CreateGhostTween(Ghost2, Ghost2.transform.position + dirG2Vector, 1.75f);
        }
        else
        {
            if (pacStudent.transform.position.sqrMagnitude < 10)
            {
                // Move Towards PacStudent
            }
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
            if (left.collider == null && right.collider == null && Ghost3.transform.position.y > 4.5)
            {
                MoveG3Direction(2, 4);
                //dirG3Vector = Vector3.up;
                //CreateGhostTween(Ghost3, Ghost3.transform.position + dirG3Vector, 1.75f);

            }
        }
        else if (down.collider == null && dirG3Vector == Vector3.down)
        {
            //CreateGhostTween(Ghost3.transform.position + dirVector, 1.75f);
            CreateGhostTween(Ghost3, Ghost3.transform.position + dirG3Vector, 1.75f);
            Debug.Log("Correct");
        }
        else if (right.collider == null && dirG3Vector == Vector3.right)
        {
            CreateGhostTween(Ghost3, Ghost3.transform.position + dirG3Vector, 1.75f);
            if (up.collider == null && left.collider == null && Ghost3.transform.position.x > 0.5)
            {
                dirG3Vector = Vector3.up;
                CreateGhostTween(Ghost3, Ghost3.transform.position + dirG3Vector, 1.75f);
                
            }
            else if (down.collider == null && left.collider == null && Ghost3.transform.position.y > 12.5)
            {
                dirG3Vector = Vector3.down;
                CreateGhostTween(Ghost3, Ghost3.transform.position + dirG3Vector, 1.75f);

            }
        }
        else if (left.collider == null && dirG3Vector == Vector3.left)
        {
            CreateGhostTween(Ghost3, Ghost3.transform.position + dirG3Vector, 1.75f);

            if (up.collider == null && Ghost3.transform.position.x < -0.5 && Ghost3.transform.position.y > 0.5)
            {
                MoveG3Direction(0,3);
                if (dirG3Vector == Vector3.up || dirG3Vector == Vector3.left)
                {
                    CreateGhostTween(Ghost3, Ghost3.transform.position + dirG3Vector, 1.75f);
                }
            }
            else if (down.collider == null && dirG3Vector != Vector3.up && Ghost3.transform.position.y > 12.5)
            {
                dirG3Vector = Vector3.down;
                CreateGhostTween(Ghost3, Ghost3.transform.position + dirG3Vector, 1.75f);
            }

        }
        
        else
        {
            if (up.collider != null && Ghost3.transform.position.y > 7.5)
            {
                MoveG3Direction(2, 4);
                //CreateGhostTween(Ghost3, Ghost3.transform.position + dirG3Vector, 1.75f);

                if (right.collider != null)
                {
                    if (dirG3Vector == Vector3.up)
                    {
                        MoveG3Direction(2, 2);
                        
                    }
                    else if (dirG3Vector == Vector3.left)
                    {
                        MoveG3Direction(1, 1);
                    }
                    else
                    {
                        MoveG3Direction(2, 2);
                    }
                }
                else if (left.collider != null)
                {
                    if (dirG3Vector == Vector3.up)
                    {
                        MoveG3Direction(3, 3);
                    }
                    else if (dirG3Vector == Vector3.right)
                    {
                        MoveG3Direction(1, 1);
                    }
                }
                else
                {
                    if (dirG3Vector == Vector3.left)
                    {
                        MoveG3Direction(2, 2);
                    }
                    else if (dirG3Vector == Vector3.right)
                    {
                        MoveG3Direction(3, 3);
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
                        MoveG3Direction(2, 2);
                    }
                    else if (dirG3Vector == Vector3.left)
                    {
                        MoveG3Direction(0, 0);
                    }
                }
                else if (left.collider != null)
                {
                    if (dirG3Vector == Vector3.down)
                    {
                        MoveG3Direction(3, 3);
                    }
                    else if (dirG3Vector == Vector3.right)
                    {
                        MoveG3Direction(0, 0);
                    }
                }
                else
                {
                    if (dirG3Vector == Vector3.left)
                    {
                        MoveG3Direction(3, 3);
                    }
                    else if (dirG3Vector == Vector3.right)
                    {
                        MoveG3Direction(2, 2);
                    }
                }
            }
            else if (up.collider == null)
            {
                dirG3Vector = Vector3.up;
            }
            else if (left.collider == null || right.collider == null)
            {
                MoveG3Direction(2, 4);
            }
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
        Debug.Log(collision.collider.name);
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
            //Debug.Log("Going Right!");
            //Ghost4Animation();
            if (up.collider == null && left.collider == null && Ghost4.transform.position.x > 0.5)
            {
                dirG4Vector = Vector3.up;
                CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);
                Debug.Log("Pizza");
            }

        }
        else if (left.collider == null && dirG4Vector == Vector3.left)
        {
            CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);

            if (down.collider == null && left.collider == null && Ghost4.transform.position.x < 0.5 && Ghost4.transform.position.x > -1.5)
            {
                dirG4Vector = Vector3.down;
                CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);
                Debug.Log("Point");
            }


        }
        else if (down.collider == null && dirG4Vector == Vector3.down)
        {
            //CreateGhostTween(Ghost3.transform.position + dirVector, 1.75f);
            CreateGhostTween(Ghost4, Ghost4.transform.position + dirG4Vector, 1.75f);
            //Debug.Log("Going Down");

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
                //Debug.Log("Going down!");
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
