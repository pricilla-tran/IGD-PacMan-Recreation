using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    private Vector3 movement;
    private float movementSqrMagnitude;
    private Quaternion rotation;
    public Animator pacStudentAnimator;
    public float walkSpeed = 1.75f;
    public AudioSource walkingSound;
    public AudioSource IntroBGMusic;
    public AudioSource BGMusic;
    private Tweener tweener;
    private Vector3 playerInitialPos;

    // Start is called before the first frame update
    void Start()
    {
        IntroBGMusic.Play();
        playerInitialPos = new Vector3(-12.5f, 13.5f, 0);
        tweener = gameObject.GetComponent<Tweener>();
        Invoke("PlayBG", 1.5f);
        gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -45);
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
        CharacterPosition();
        CharacterRotation();
        WalkingAnimation();
    }

    void PlayBG()
    {
        BGMusic.Play();
    }

    void GetMovementInput()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        movement = Vector3.ClampMagnitude(movement, 1.0f);
        movementSqrMagnitude = movement.sqrMagnitude;
        //Debug.Log(movement);
    }

    void CharacterPosition()
    {
        gameObject.transform.Translate(walkSpeed * movement * Time.deltaTime, Space.World);
    }

    void CharacterRotation()
    {
        //Vector3 relativePos = movement - transform.position;
        //Quaternion.LookRotation();
        
        if (movement != playerInitialPos)
        {
            // Find a way to make it fixed at 90 degrees 
            rotation = Quaternion.FromToRotation(gameObject.transform.position, movement);
            //rotation = transform.rotation * Quaternion.Euler(0.0f, 0.0f, 90); 
            gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, walkSpeed);
            
        }
        

        /*
        if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") == -1 && !Input.GetButton("Horizontal"))
        {
            pacStudentAnimator.SetTrigger("UpParam");
            //CreateTween(movement, 1.5f);
        }

        // (-15.5, 8.5, 0)
        else if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") == 1 && !Input.GetButton("Horizontal"))
        {
            pacStudentAnimator.SetTrigger("DownParam");
            //CreateTween(new Vector3(-7.5f, 9.5f, 0), 1.5f);
        }

        // Left
        else if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") == -1 && !Input.GetButton("Vertical"))
        {
            pacStudentAnimator.SetTrigger("LeftParam");
            //CreateTween(new Vector3(-12.5f, 9.5f, 0), 1.5f);
        }

        // Right
        else if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") == 1 && !Input.GetButton("Vertical"))
        {
            pacStudentAnimator.SetTrigger("RightParam");
            //CreateTween(new Vector3(-7.5f, 13.5f, 0), 1.5f);
        }
        */

    }

    void WalkingAnimation()
    {
        //pacStudentAnimator = gameObject.GetComponent<Animator>();
        //pacStudentAnimator.SetFloat("MoveSpeed", movementSqrMagnitude);
        //if
        pacStudentAnimator.speed = movementSqrMagnitude;
    }

    void WalkingAudio()
    {

    }

    public void CreateTween(Vector3 endPosition, float duration)
    {
        bool addedTween = tweener.AddTween(gameObject.transform, gameObject.transform.position, endPosition, duration);   
    }

}
