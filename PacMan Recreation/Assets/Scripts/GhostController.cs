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
    private Vector3 dirVector;

    // Start is called before the first frame update
    void Start()
    {
        ghost4Pos = new Vector3[4];
        ghost4Pos[0] = new Vector3(0.5f, 3.5f, 0);
        ghost4Pos[1] = new Vector3(4.5f, 3.5f, 0);
        ghost4Pos[2] = new Vector3(4.5f, -0.5f, 0);
        ghost4Pos[3] = new Vector3(7.5f, -0.5f, 0);


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

        MoveDirection();

    }

    // Update is called once per frame
    void Update()
    {
        Ghost3Move();
        
    }

    public void ScaredState()
    {
        Ghost1Animator.ResetTrigger("Down");
        Ghost1Animator.ResetTrigger("Up");
        Ghost1Animator.ResetTrigger("Left");
        Ghost1Animator.ResetTrigger("Right");
        Ghost1Animator.SetTrigger("Scared");
        //Ghost2Animator.SetTrigger("Scared");
        //Ghost3Animator.SetTrigger("Scared");
        //Ghost4Animator.SetTrigger("Scared");

    }

    public void Ghost1Move()
    {
        // Move in a random valid direction that is further than or equal to the distance from Pacstudent to the current grid position 
        
    }

    public void Ghost2Move()
    {
        // Move to a random adjacent junction that is closer than or equal to PacStudent than the current grid position 



    }

    public void Ghost3Move()
    {
        // Move randomly at grid position
        
        RaycastHit2D right = Physics2D.Raycast(gameObject.transform.position, Vector2.right, 1.0f);
        RaycastHit2D left = Physics2D.Raycast(gameObject.transform.position, Vector2.left, 1.0f);
        RaycastHit2D down = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 1.0f);
        RaycastHit2D up = Physics2D.Raycast(gameObject.transform.position, Vector2.up, 1.0f);
        
        //if (right.collider == null || left.collider == null)
        //{
            CreateGhostTween(Ghost3.transform.position + dirVector, 1.75f);
        //}
        

        //if ()
        //CreateGhostTween(Ghost3.transform.position + dirVector, 1.75f);

    }

    private void MoveDirection()
    {
        int direction = Random.Range(0, 4);
        switch(direction)
        {
            case 0:
                // Up
                dirVector = Vector3.up;
                break;
            case 1:
                // Down 
                dirVector = Vector3.down;
                break;
            case 2:
                // Left
                dirVector = Vector3.left;
                break;
            case 3:
                // Right
                dirVector = Vector3.right;
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
        /*
        RaycastHit2D right = Physics2D.Raycast(gameObject.transform.position, Vector2.right, 1.0f);
        RaycastHit2D left = Physics2D.Raycast(gameObject.transform.position, Vector2.left, 1.0f);
        RaycastHit2D down = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 1.0f);
        RaycastHit2D up = Physics2D.Raycast(gameObject.transform.position, Vector2.up, 1.0f);

        if (right.collider.tag == "Walls")
        {

        }
        else if (left.collider.tag == "Walls")
        {

        }
        else if (up.collider.tag == "Walls")
        {

        }
        else if (down.collider.tag == "Walls")
        {

        }
        */

        //CreateGhostTween(new Vector3(0.5f, 3.5f, 0), 2f);
        //CreateGhostTween(ghost4Pos[1], 2f);
        StartCoroutine(clockwiseWallMove());

    }

    private IEnumerator clockwiseWallMove()
    {
        //return null;
        while (cycleCount == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                CreateGhostTween(ghost4Pos[i], 2f);
            }
            yield return new WaitForSeconds(0.5f);
        }

    }

    public void CreateGhostTween(Vector3 endPosition, float duration)
    {
        //if (endPosition.x > -13.5f && endPosition.x < 13.5f && endPosition.y > -14.5f && endPosition.y < 14.5f)
        //{
        bool addedTween = ghost3Tweener.AddTween(Ghost3.transform, Ghost3.transform.position, endPosition, duration);
        //}
    }

}
