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

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        Ghost4Move();
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
    }

    public void Ghost4Move()
    {
        // Move clockwise around the map, following the outside wall
        RaycastHit2D forward = Physics2D.Raycast(Ghost4.transform.position, Vector2.up, 1.0f);
        if (forward.collider.tag == "Walls")
        {
            Debug.Log("Wallllllll");
        }
    }

    public void CreateGhostTween(Vector3 endPosition, float duration)
    {
        //if (endPosition.x > -13.5f && endPosition.x < 13.5f && endPosition.y > -14.5f && endPosition.y < 14.5f)
        //{
        bool addedTween = ghost1Tweener.AddTween(gameObject.transform, gameObject.transform.position, endPosition, duration);
        //}
    }

}
