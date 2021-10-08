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
    private Quaternion initialRotation;
    private float timer;

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
        //CharacterRotation();
        WalkingAnimation();
        WalkingAudio();

        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") == -1)
        {
            StartCoroutine(CharacterRotate(45.0f));
            //StartCoroutine(CharacterMove(movement));
        }
        else if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") == 1)
        {
            StartCoroutine(CharacterRotate(-135.0f));
            //StartCoroutine(CharacterMove(movement));
        }
        else if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") == 1)
        {
            StartCoroutine(CharacterRotate(-45.0f));
        }
        else if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") == -1)
        {
            StartCoroutine(CharacterRotate(135.0f));
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
        CreateTween(gameObject.transform.position + movement, walkSpeed);

    }

    private IEnumerator CharacterMove(Vector3 endPos)
    {
        //if (movement != playerInitialPos)
        //{
        //CreateTween(movement, walkSpeed);
        //gameObject.transform.Translate(walkSpeed * movement * Time.deltaTime, Space.World);
        //}
        CreateTween(gameObject.transform.position + movement, walkSpeed);
        yield return new WaitForSeconds(walkSpeed);
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
            BGMusic.volume = 0.2f;
        }
        else if (movementSqrMagnitude <= 0.3f && walkingSound.isPlaying)
        {
            walkingSound.Stop();
            BGMusic.volume = 0.5f;
        }
    }

    public void CreateTween(Vector3 endPosition, float duration)
    {
        bool addedTween = tweener.AddTween(gameObject.transform, gameObject.transform.position, endPosition, duration);   
    }

}
