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
    private string lastInput;
    private string currentInput;
    private float timer;
    public ParticleSystem dust;

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
        //CharacterPosition();
        //CharacterRotation();
        WalkingAnimation();
        WalkingAudio();

        // Left
        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") == -1)
        {
            StartCoroutine(CharacterRotate(45.0f));
            //StartCoroutine(CharacterMove(movement));
            lastInput = "left";
            //CharacterPosition();
            StartCoroutine(CharacterMove(movement));

        }
        // Right
        else if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") == 1)
        {
            StartCoroutine(CharacterRotate(-135.0f));
            //StartCoroutine(CharacterMove(movement));
            lastInput = "right";
            CharacterPosition();
            //StartCoroutine(CharacterMove(movement));
        }
        // Up
        else if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") == 1)
        {
            StartCoroutine(CharacterRotate(-45.0f));
            lastInput = "up";
            CharacterPosition();
            //StartCoroutine(CharacterMove(movement));
        }
        // Down
        else if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") == -1)
        {
            StartCoroutine(CharacterRotate(135.0f));
            lastInput = "down";
            CharacterPosition();
            //StartCoroutine(CharacterMove(movement));
        }

    }

    void PlayBG()
    {
        BGMusic.Play();
    }

    void GetMovementInput()
    {
        timer += Time.deltaTime;
        movement = new Vector3((int) Input.GetAxis("Horizontal"), (int)Input.GetAxis("Vertical"), 0);
        movement = Vector3.ClampMagnitude(movement, 1.0f);
        movementSqrMagnitude = movement.sqrMagnitude;
        //Debug.Log(movement);
        //CreateTween(movement, 1.5f);
    }

    void CharacterPosition()
    {
        //gameObject.transform.Translate(walkSpeed * movement * Time.deltaTime, Space.World);
        //CreateTween(gameObject.transform.position + movement, walkSpeed);
        
        if (lastInput == "up")
        {
            //CreateTween(gameObject.transform.position + movement, walkSpeed);
            //if (gameObject.transform.position + Vector3.forward)
            currentInput = "up";
            StartCoroutine(CharacterMove(movement));
        }
        else if (lastInput == "down")
        {
            //CreateTween(gameObject.transform.position + movement, walkSpeed);
            currentInput = "down";
            StartCoroutine(CharacterMove(movement));
        }
        else if (lastInput == "left")
        {
            //CreateTween(gameObject.transform.position + Vector3.left, walkSpeed);
            currentInput = "left";
            StartCoroutine(CharacterMove(movement));
        }
        else if (lastInput == "right")
        {
            currentInput = "right";
            StartCoroutine(CharacterMove(movement));
            //CreateTween(gameObject.transform.position + Vector3.right, walkSpeed);
        }
        

    }

    private IEnumerator CharacterMove(Vector3 endPos)
    {
        //if (movement != playerInitialPos)
        //{
        //CreateTween(movement, walkSpeed);
        //gameObject.transform.Translate(walkSpeed * movement * Time.deltaTime, Space.World);
        //}
        //CreateTween(gameObject.transform.position + movement, walkSpeed);
        if (currentInput == "up")
        {
            CreateTween(gameObject.transform.position + movement, walkSpeed);
            yield return new WaitForSeconds(walkSpeed);
        }
        else if (currentInput == "down")
        {
            CreateTween(gameObject.transform.position + movement, walkSpeed);
            yield return new WaitForSeconds(walkSpeed);
        }
        else if (currentInput == "left")
        {
            CreateTween(gameObject.transform.position + movement, walkSpeed);
            yield return new WaitForSeconds(0.5f);
        }
        else if (currentInput == "right")
        {
            CreateTween(gameObject.transform.position + movement, walkSpeed);
            yield return new WaitForSeconds(0.5f);
        }

        
    }

    void CharacterRotation()
    {
        //timer += Time.deltaTime;

        if (movement != playerInitialPos)
        {
            // Find a way to make it fixed at 90 degrees 
            //rotation = Quaternion.FromToRotation(gameObject.transform.position, movement * 90);
            rotation = transform.rotation * Quaternion.Euler(0.0f, 0.0f, 90); 
            gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, rotation, walkSpeed);
            
        }

    }

    private IEnumerator CharacterRotate(float angle)
    {
        if (movement != playerInitialPos)
        {
            float currRot = gameObject.transform.rotation.eulerAngles.z;
            rotation = Quaternion.Euler(0.0f, 0.0f, angle);
            gameObject.transform.rotation = rotation;
        }
        yield return new WaitForSeconds(0.5f);
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
        if (movementSqrMagnitude > 0.25f && !walkingSound.isPlaying)
        {
            walkingSound.volume = movementSqrMagnitude;
            walkingSound.Play();
            dust.Play();
            BGMusic.volume = 0.2f;
        }
        else if (movementSqrMagnitude <= 0.3f && walkingSound.isPlaying)
        {
            walkingSound.Stop();
            dust.Stop();
            BGMusic.volume = 0.5f;
        }
    }

    public void CreateTween(Vector3 endPosition, float duration)
    {
        bool addedTween = tweener.AddTween(gameObject.transform, gameObject.transform.position, endPosition, duration);   
    }

}
